using comics_shelf_api.core.Consts;
using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.ExternalProviders;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Repositories.Interfaces;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Services
{
    public class PurchaseComicsService : IPurchaseComicsService
    {
        private readonly IPurchaseComicsRepository _purchaseComicsRepository;
        private readonly IComicsProvider _comicsProvider;
        public PurchaseComicsService(IPurchaseComicsRepository purchaseComicsRepository, IComicsProvider comicsProvider) {
            this._purchaseComicsRepository = purchaseComicsRepository;
            this._comicsProvider = comicsProvider;
        }
        public async Task<ApiResult<FileDto>> CanUserDownloadFileAsync(Guid userId, Guid comicsId)
        {
            try
            {
                var purcharsedComics = await _purchaseComicsRepository.GetPurchasedComicsByUserAsync(userId, comicsId);
                if (purcharsedComics == null) {
                    return new ApiResult<FileDto>()
                    {
                        Result = new FileDto() { 
                            CanUserDownload = false,
                            File = null
                        },
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
                return new ApiResult<FileDto>()
                {
                    Result = new FileDto()
                    {
                        CanUserDownload = true,
                        File = null
                    },
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex) {
                return new ApiResult<FileDto>()
                {
                    Error = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        public Task<ApiResult<FileDto>> DownloadFileAsync(Guid comicsId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedResult<ExternalProviderComicsDto>>> GetAvailableComicsAsync(int currentPage)
        {
            try {
                var result = await _comicsProvider.GetComicsFromProvider();
                var filtered = await this.GetComicsWitoutExclusiveAsync(result);
                var paged = PagedResult<ExternalProviderComicsDto>.Create(filtered.AsQueryable(), currentPage, 10, "ReleaseDate", Enums.OrderBy.Desc);
                return new ApiResult<PagedResult<ExternalProviderComicsDto>>()
                {
                    Result = paged,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex) { 
                return new ApiResult<PagedResult<ExternalProviderComicsDto>>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Error = ex.Message
                };
            }
            
        }

        public async Task<ApiResult<List<Comics>>> GetUserComicsAsync(Guid userId)
        {
            try
            {
                var result = await _purchaseComicsRepository.GetUserComicsAsync(userId);
                return new ApiResult<List<Comics>>()
                {
                    Result = result,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex) {
                return new ApiResult<List<Comics>>() {
                    Error = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<ApiResult<PurchasedComicsUsers>> PurchaseComicsAsync(Guid userId, ExternalProviderComicsDto externalProviderComicsDto)
        {
            try
            {
                var currentUserIsOwnerOfComics = await CurrentUserHaveSpecificComics(userId, externalProviderComicsDto.DiamondId);
                if (currentUserIsOwnerOfComics) {
                    throw new Exception(String.Format(PurchaseComicsServiceError.USER_HAVE_COMICS, externalProviderComicsDto.DiamondId));
                }
                    var result = await _purchaseComicsRepository.PurchaseComicsAsync(userId, externalProviderComicsDto);
                return new ApiResult<PurchasedComicsUsers>()
                {
                    Result = result,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex) {
                return new ApiResult<PurchasedComicsUsers>()
                {
                    Error = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<ApiResult<object>> ReturnComicsAsync(Guid userId, Guid comics)
        {
            try
            {
                var purcharsedComics = await _purchaseComicsRepository.GetPurchasedComicsByUserAsync(userId, comics);
                if (purcharsedComics == null) {
                    throw new Exception(String.Format(PurchaseComicsServiceError.CANNOT_FIND_PURCHASED_COMICS_WITH_ID, comics));
                }
                await _purchaseComicsRepository.ReturnComicsAsync(purcharsedComics);
                return new ApiResult<object>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex) {
                return new ApiResult<object>()
                {
                    Error = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }

        private async Task<List<ExternalProviderComicsDto>> GetComicsWitoutExclusiveAsync(List<ExternalProviderComicsDto> comics) {
            var exclusives = await _purchaseComicsRepository.GetComicsPurchasedAsExclusiveAsync();
            if (!exclusives.Any()) return comics;
            return comics.Where(x => !exclusives.Any(y => y == x.DiamondId)).ToList();
        }

        private async Task<bool> CurrentUserHaveSpecificComics(Guid userId,string diamondId) {
            var result = await _purchaseComicsRepository.GetPurchasedComicsByUserAsync(userId, diamondId);
            return result != null ? true : false;
        }
    }
}
