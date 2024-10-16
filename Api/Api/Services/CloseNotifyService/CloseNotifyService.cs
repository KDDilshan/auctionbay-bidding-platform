using Api.Data;
using Api.Models.Email;
using Api.Services.EmailService;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.CloseNotifyService
{
    public class CloseNotifyService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CloseNotifyService> _logger;

        public CloseNotifyService(IServiceProvider serviceProvider, ILogger<CloseNotifyService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                using (var scope = _serviceProvider.CreateScope()) 
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var auctions = _context.Auctions.Where(a => a.EndDate < DateTime.UtcNow && a.Status == "Open").ToList();

                    foreach (var auction in auctions)
                    {
                        auction.Status = "Close";
                        _context.Auctions.Update(auction);
                        await _context.SaveChangesAsync();

                        var owner = _context.Users.Find(auction.UserID);
                        var email = new Email
                        {
                            to = owner.Email,
                            subject = "Auction Closed",
                            body = $"Your auction {auction.Title} has closed."
                        };
                        
                        var highestBid = await _context.Bids.Where(b => b.AuctionID == auction.Id).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();

                        if (highestBid != null)
                        {
                            var winner = await _context.Users.FindAsync(highestBid.UserId);
                            email.body += $"The winner is {winner.Email} with a bid of {highestBid.BidPrice}.";
                        }

                        _emailService.SendEmail(email);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

    }
}
