using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.ExternalProviders;
using comics_shelf_api.core.Repositories.Interfaces;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace comics_shelf_api.core.Services
{
    public class ComicsService: IComicsService
    {
        private readonly IComicsRepository _comicsRepository;
        private readonly IComicsProvider _comicsProvider;
        private readonly IPurchaseComicsRepository _purchaseComicsRepository;

        public ComicsService(IComicsRepository comicsRepository, IComicsProvider comicsProvider,
            IPurchaseComicsRepository purchaseComicsRepository) {
            this._comicsRepository = comicsRepository;
            this._comicsProvider = comicsProvider;
            this._purchaseComicsRepository = purchaseComicsRepository;
        }

        public async Task<ApiResult<PagedResult<ExternalProviderComicsDto>>> GetAvailableComicsAsync(int currentPage)
        {
            try
            {
                var filtered = await GetFilteredComics();
                var paged = PagedResult<ExternalProviderComicsDto>.Create(filtered.AsQueryable(), currentPage, 10, "ReleaseDate", Enums.OrderBy.Desc);
                return new ApiResult<PagedResult<ExternalProviderComicsDto>>()
                {
                    Result = paged,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<PagedResult<ExternalProviderComicsDto>>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Error = ex.Message
                };
            }

        }

        private async Task<List<ExternalProviderComicsDto>> GetFilteredComics() {
            var result = await _comicsProvider.GetComicsFromProvider();
            return await this.GetComicsWitoutExclusiveAsync(result);
        }

        private async Task<List<ExternalProviderComicsDto>> GetComicsWitoutExclusiveAsync(List<ExternalProviderComicsDto> comics)
        {
            var exclusives = await _purchaseComicsRepository.GetComicsPurchasedAsExclusiveAsync();
            if (!exclusives.Any()) return comics;
            return comics.Where(x => !exclusives.Any(y => y == x.DiamondId)).ToList();
        }

        public async Task<ApiResult<List<ExternalProviderComicsDto>>> GetComicsByTitle(string title)
        {
            try
            {
                var filtered = await GetFilteredComics();
                var founded = filtered.Where(x => x.Title.Contains(title)).Take(5).ToList();
                return new ApiResult<List<ExternalProviderComicsDto>>() {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = founded
                };
            }
            catch (Exception ex) {
                return new ApiResult<List<ExternalProviderComicsDto>>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Error = ex.Message
                };
            }
            
        }
    }
}
