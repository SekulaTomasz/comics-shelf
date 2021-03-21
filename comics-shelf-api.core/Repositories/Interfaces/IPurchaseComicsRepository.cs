using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Repositories.Interfaces
{
    public interface IPurchaseComicsRepository
    {

        Task<List<string>> GetComicsPurchasedAsExclusiveAsync();
        Task<List<Comics>> GetUserComicsAsync(Guid userId);
        Task<PurchasedComicsUsers> PurchaseComicsAsync(User user, Comics comics, bool asExclusive = false);
        Task ReturnComicsAsync(PurchasedComicsUsers comics);
        Task<PurchasedComicsUsers> GetPurchasedComicsByUserAsync(Guid userId, Guid comicsId);

        Task<PurchasedComicsUsers> GetPurchasedComicsByUserAsync(Guid userId, string diamondId);
    }
}
