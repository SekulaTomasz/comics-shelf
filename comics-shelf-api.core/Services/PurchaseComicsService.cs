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
        private readonly IUserRepository _userRepository;
        private readonly IComicsRepository _comicsRepository;
        public PurchaseComicsService(IPurchaseComicsRepository purchaseComicsRepository,
            IUserRepository userRepository, IComicsRepository comicsRepository) {
            this._purchaseComicsRepository = purchaseComicsRepository;
            this._userRepository = userRepository;
            this._comicsRepository = comicsRepository;
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

        public async Task<ApiResult<PurchasedComicsUsers>> PurchaseComicsAsync(Guid userId, ExternalProviderComicsDto externalProviderComicsDto, bool asExclusive = false)
        {
            try
            {
                var currentUserIsOwnerOfComics = await CurrentUserHaveSpecificComics(userId, externalProviderComicsDto.DiamondId);
                if (currentUserIsOwnerOfComics) {
                    throw new Exception(String.Format(PurchaseComicsServiceError.USER_HAVE_COMICS, externalProviderComicsDto.DiamondId));
                }
                var user = await _userRepository.FindUserByIdAsync(userId);
                if (user == null) throw new Exception(String.Format(UserServiceError.USER_NOT_FOUND, userId));
                var comics = await _comicsRepository.GetComicsByExternalApiId(externalProviderComicsDto.DiamondId);
                if(comics == null)
                {
                    var toCreate = new Comics() { 
                        Id = Guid.NewGuid(),
                        Creators = externalProviderComicsDto.Creators,
                        Description = externalProviderComicsDto.Description,
                        DiamondId = externalProviderComicsDto.DiamondId,
                        Price = externalProviderComicsDto.Price,
                        Publisher = externalProviderComicsDto.Publisher,
                        ReleaseDate = externalProviderComicsDto.ReleaseDate,
                        Title = externalProviderComicsDto.Title
                    };
                    comics = await _comicsRepository.CreateNewComics(toCreate);
                }
                var result = await _purchaseComicsRepository.PurchaseComicsAsync(user, comics, asExclusive);
                user.Coins -= result.Cost;
                await _userRepository.UpdateUser(user);
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
                var user = await _userRepository.FindUserByIdAsync(userId);
                if (user == null) throw new Exception(String.Format(UserServiceError.USER_NOT_FOUND, userId));
                var purcharsedComics = await _purchaseComicsRepository.GetPurchasedComicsByUserAsync(userId, comics);
                if (purcharsedComics == null) {
                    throw new Exception(String.Format(PurchaseComicsServiceError.CANNOT_FIND_PURCHASED_COMICS_WITH_ID, comics));
                }

                if (!CheckIfComicsWasBoughtLessThanOneWeek(purcharsedComics)) throw new Exception(PurchaseComicsServiceError.CANNOT_RETURN_COMICS_WAS_BOUGHT_MORE_THAN_ONE_WEEK);

                await _purchaseComicsRepository.ReturnComicsAsync(purcharsedComics);
                user.Coins += purcharsedComics.Cost;
                await _userRepository.UpdateUser(user);
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

        private async Task<bool> CurrentUserHaveSpecificComics(Guid userId, string diamondId) {
            var result = await _purchaseComicsRepository.GetPurchasedComicsByUserAsync(userId, diamondId);
            return result != null;
        }

        private bool CheckIfComicsWasBoughtLessThanOneWeek(PurchasedComicsUsers purchasedComics) {
            var currentDate = DateTime.UtcNow;

            if (purchasedComics.PurchaseDate < currentDate.AddDays(-7)) {
                return false;
            }
            return true;
        }
    }
}
