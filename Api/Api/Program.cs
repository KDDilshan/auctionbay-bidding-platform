using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Api.Data;
using Api.Entities;
using Api.Services.EmailService;
using MailKit.Net.Smtp;
using Api.Services.FileService;
using Microsoft.Extensions.FileProviders;
using Api.Services.JwtService;
using Api.Services.UserService;
using Stripe;
using Api.Services.NftService;
using Newtonsoft.Json;
using Api.Services.AuctionService;
using Api.Services.PaymentService;
using Api.Dtos;
using Api.Services.BidService;
using Api.Services.CloseNotifyService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var JWTSetting = builder.Configuration.GetSection("JWTSetting");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();



builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt => {
    opt.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Avoid default behavior
            context.HandleResponse();

            if (!context.Response.HasStarted)
            {
                // If the user is not authenticated, return a 401
                context.Response.StatusCode = 401; // Unauthorized
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new { message = "You are not authenticated" });
                return context.Response.WriteAsync(result);
            }

            return Task.CompletedTask;
        },

        OnForbidden = context =>
        {
            // Handle 403 Forbidden cases for roles
            context.Response.StatusCode = 403; // Forbidden
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new { message = "You do not have permission to access this resource" });
            return context.Response.WriteAsync(result);
        }
    };

    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = JWTSetting["ValidAudience"],
        ValidIssuer = JWTSetting["ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting.GetSection("securityKey").Value!))
    };
});




builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization Example: 'Bearer {your_token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",  // Corrected from 'outh2' to 'Bearer'
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBidService, BidService>();

builder.Services.AddScoped<INftRepository, NftRepository>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddScoped<AuctionService>();

builder.Services.AddTransient<SmtpClient>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddSingleton<StripePaymentService>();

builder.Services.AddTransient<IFileService, Api.Services.FileService.FileService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});

// Auction auto close service
builder.Services.AddHostedService<CloseNotifyService>();

var app = builder.Build();

app.Lifetime.ApplicationStopping.Register(() =>
{
    app.Services.GetRequiredService<SmtpClient>().Disconnect(true);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/wwwroot"
});

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    await SeedRolesAsync(roleManager);
    await SeedDefaultAdminAsync(userManager, roleManager);
}

app.Run();

async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "Admin", "Seller", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

async Task SeedDefaultAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var defaultAdminEmail = "admin@yourapp.com";
    var defaultAdminPassword = "Admin123!"; 

    var adminUser = await userManager.FindByEmailAsync(defaultAdminEmail);

    if (adminUser == null)
    {
        var newAdmin = new AppUser
        {
            UserName = defaultAdminEmail,
            Email = defaultAdminEmail,
            EmailConfirmed = true
        };

        var createAdminResult = await userManager.CreateAsync(newAdmin, defaultAdminPassword);

        if (createAdminResult.Succeeded)
        {
            // Assign Admin role
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}
