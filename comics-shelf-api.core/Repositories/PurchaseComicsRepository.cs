using comics_shelf_api.core.Database;
using comics_shelf_api.core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Dtos;

namespace comics_shelf_api.core.Repositories
{
    public class PurchaseComicsRepository : IPurchaseComicsRepository
    {
        private readonly DatabaseContext _context;
        public PurchaseComicsRepository(DatabaseContext context) {
            this._context = context;
        }

        public async Task<PurchasedComicsUsers> GetPurchasedComicsByUserAsync(Guid userId, Guid comicsId) {
            return await _context.PurchasedComicsUsers.FirstOrDefaultAsync(x => x.User.Id == userId && x.Comics.Id == comicsId);
        }

        public async Task<PurchasedComicsUsers> GetPurchasedComicsByUserAsync(Guid userId, string diamondId)
        {
            return await _context.PurchasedComicsUsers.FirstOrDefaultAsync(x => x.User.Id == userId && x.Comics.DiamondId == diamondId);
        }

        public async Task<List<string>> GetComicsPurchasedAsExclusiveAsync()
        {
            return await _context.PurchasedComicsUsers.Where(x => x.PurchasedAsExclusive)
                .Select(x => x.Comics.DiamondId).ToListAsync();
        }

        public async Task<List<Comics>> GetUserComicsAsync(Guid userId)
        {
            return await _context.PurchasedComicsUsers.Where(x => x.User.Id == userId).Select(x => x.Comics).ToListAsync();
        }

        public async Task<PurchasedComicsUsers> PurchaseComicsAsync(User user, Comics comics, bool asExclusive = false)
        {
            _context.Database.BeginTransaction();
            var purchaseComics = new PurchasedComicsUsers()
            {
                Id = Guid.NewGuid(),
                Comics = comics,
                Cost = 5,
                PurchasedAsExclusive = asExclusive,
                User = user,
                PurchaseDate = DateTime.UtcNow
            };
            _context.PurchasedComicsUsers.Add(purchaseComics);
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
            var savedResult = await _context.PurchasedComicsUsers.FindAsync(purchaseComics.Id);
            return savedResult;
        }

        public async Task ReturnComicsAsync(PurchasedComicsUsers comics)
        {
            _context.Database.BeginTransaction();
            _context.PurchasedComicsUsers.Remove(comics);
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
            
        }
    }
}
