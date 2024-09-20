using System.Reflection.Emit;
using Api.Dtos;
using Api.Entities;
using Api.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Api.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Nft> Nfts { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Auction -> Bids (One Auction has many Bids)
            builder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionID)
                .OnDelete(DeleteBehavior.NoAction);


            // AppUser -> Nfts (One User can have many Nfts)
            builder.Entity<AppUser>()
                .HasMany(u => u.nfts)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // AppUser -> Auctions (One User can create many Auctions)
            builder.Entity<AppUser>()
                .HasMany(u => u.auctions)
                .WithOne(a => a.AppUser)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // AppUser -> Bids (One User can place many Bids)
            builder.Entity<AppUser>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.AppUsers)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // AppUser -> Request (One User can have one Request)
            builder.Entity<AppUser>()
                .HasOne(u => u.Requests)
                .WithOne(r => r.User)
                .HasForeignKey<Request>(r => r.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // Nft -> Auctions (One Nft can have many Auctions)
            builder.Entity<Nft>()
                .HasMany(n => n.Auctions)
                .WithOne(a => a.Nft)
                .HasForeignKey(a => a.NftId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(builder);

            //seeding nft to database
            const int amountOfProductsToSeed = 5;
            var productsToSeed = new Nft[amountOfProductsToSeed];
            for (int i = 1; i <= amountOfProductsToSeed; i++)
            {
                productsToSeed[i - 1] = new Nft
                {
                    Id = i,
                    Title = $"Product {i}",
                    Description = $"Product {i} description. This is an amazing product with a price-quality balance you won't find anywhere ele.",
                    Price = 1000 * i,
                    UserId = "52d7665b-c5d8-4324-8975-0641870a4b53"
                };
            }
            builder.Entity<Nft>().HasData(productsToSeed);


            // Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Buyer", NormalizedName = "BUYER" },
                new IdentityRole { Id = "3", Name = "Seller", NormalizedName = "SELLER" }
            );

            //Seed auctiondetais
            builder.Entity<Auction>().HasData(
                new Auction
                {
                    Id=1,
                    Title = "Sample Auction",
                    Description = "This is a sample auction for an NFT.",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    Price = 500,//string price
                    NftId = 1,  // Assign the existing or newly created Nft Id
                    UserID = "52d7665b-c5d8-4324-8975-0641870a4b53", // Assign the existing or newly created AppUser Id
                   
                }
            );

            //Seed Bidding
            builder.Entity<Bid>().HasData(
                new Bid
                {
                    Id = 1,
                    BidDate = DateTime.Now.AddMinutes(10), // For example, 10 minutes after the first bid
                    BidPrice = 2500, // Another bid
                    AuctionID = 1, // Same auction
                    UserId = "ac20c689-a227-41e9-a7e2-c475194510ab"
                },
                new Bid
                {
                    Id = 2,
                    BidDate = DateTime.Now.AddMinutes(15), // For example, 10 minutes after the first bid
                    BidPrice = 3000, // Another bid
                    AuctionID = 1, // Same auction
                    UserId = "c0eba42d-ff03-449f-bf08-b3d650c5dbeb"
                }
            );


            

        }
    }
}
