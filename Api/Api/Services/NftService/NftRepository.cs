using Api.Data;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.NftService
{
    public class NftRepository : INftRepository
    {
        public readonly AppDbContext _context;

        public NftRepository(AppDbContext context) 
        {
            _context = context; 
        }

        public bool CreateNft(Nft nft)
        {
            _context.Add(nft);
            return Save();
        }

        public bool DeleteNft(Nft nft)
        {
            _context.Remove(nft);
            return Save();
        }

        public Task<Nft> GetBidsForNft(int nftId)
        {
            return _context.Nfts
                .Include(n => n.Auctions)
                .ThenInclude(a => a.Bids)
                .FirstOrDefaultAsync(n=>n.Id == nftId);
        }

        public Nft GetNftById(int id)
        {
            return _context.Nfts.Where(n => n.Id == id).FirstOrDefault();
        }

        public ICollection<Nft> GetNfts()
        {
            return _context.Nfts.ToList();
        }


        public bool NftExist(int id)
        {
            return _context.Nfts.Any(n => n.Id == id);
        }

        public bool Save()
        {
            var Saved = _context.SaveChanges();
            return Saved > 0 ? true : false;
        }

        public bool UpdateNft(Nft nft)
        {
            _context.Update(nft);
            return Save();
        }
    }
}
