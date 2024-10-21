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

                        var owner = _context.Users.Find(auction.UserID);
                        
                        var highestBid = await _context.Bids.Where(b => b.AuctionID == auction.Id).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();

                        if (highestBid != null)
                        {
                            var winner = await _context.Users.FindAsync(highestBid.UserId);
                            auction.Winner = highestBid.UserId;
                            _emailService.Send(new AuctionWinnerEmail(auction.Title,highestBid.BidPrice, winner.FirstName + " " + winner.LastName, winner.Email, "http://localhost:3000/"));
                            _emailService.Send(new AuctionClosedEmail(auction.Title, auction.EndDate, highestBid.BidPrice, winner.FirstName + " " + winner.LastName, owner.Email));
                        }
                        else
                        {
                            auction.Status = "Over";
                            _emailService.Send(new AuctionClosedNoBidsEmail(auction.Title, auction.EndDate, owner.FirstName + " " + owner.LastName, owner.Email));
                        }

                        _context.Auctions.Update(auction);
                        await _context.SaveChangesAsync();

                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

    }
}
