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

        public async Task<PurchasedComicsUsers> PurchaseComicsAsync(Guid userId, ExternalProviderComicsDto externalProviderComicsDto, bool asExclusive = false)
        {
            _context.Database.BeginTransaction();
            var user = await _context.Users.FindAsync(userId);
            var comics = new Comics()
            {
                Id = Guid.NewGuid(),
                Creators = externalProviderComicsDto.Creators,
                Description = externalProviderComicsDto.Description,
                DiamondId = externalProviderComicsDto.DiamondId,
                Price = externalProviderComicsDto.Price,
                Publisher = externalProviderComicsDto.Publisher,
                ReleaseDate = externalProviderComicsDto.ReleaseDate,
                Title = externalProviderComicsDto.Title
            };
            _context.Comics.Add(comics);
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
            
            user.Coins -= purchaseComics.Cost;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
            var savedResult = await _context.PurchasedComicsUsers.FindAsync(purchaseComics.Id);
            return savedResult;
        }

        public async Task ReturnComicsAsync(PurchasedComicsUsers comics)
        {
            var user = await  _context.Users.FindAsync(comics.User.Id);
            _context.Database.BeginTransaction();
            _context.PurchasedComicsUsers.Remove(comics);
            user.Coins += comics.Cost;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
            
        }
    }
}
