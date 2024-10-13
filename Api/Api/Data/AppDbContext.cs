using Api.Entities;
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
        public DbSet<SellerRequest> Requests { get; set; }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Auction -> Bids (One Auction has many Bids)
            builder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionID)
                .OnDelete(DeleteBehavior.Cascade);


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
                .OnDelete(DeleteBehavior.Cascade);

            // AppUser -> Request (One User can have one Request)
            builder.Entity<AppUser>()
                .HasOne(u => u.Requests)
                .WithOne(r => r.User)
                .HasForeignKey<SellerRequest>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Nft -> Auctions (One Nft can have many Auctions)
            builder.Entity<Nft>()
                .HasMany(n => n.Auctions)
                .WithOne(a => a.Nft)
                .HasForeignKey(a => a.NftId)
               .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(builder);
        }
    }
}
