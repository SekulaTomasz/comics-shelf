using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Services.Interfaces
{
    public interface IPurchaseComicsService
    {
        Task<ApiResult<PurchasedComicsUsers>> PurchaseComicsAsync(Guid userId, ExternalProviderComicsDto externalProviderComicsDto);
        Task<ApiResult<FileDto>> DownloadFileAsync(Guid comicsId);
        Task<ApiResult<FileDto>> CanUserDownloadFileAsync(Guid userId, Guid comicsId);
        Task<ApiResult<object>> ReturnComicsAsync(Guid userId, Guid comics);
        Task<ApiResult<List<Comics>>> GetUserComicsAsync(Guid userId);
        Task<ApiResult<PagedResult<ExternalProviderComicsDto>>> GetAvailableComicsAsync(int currentPage);
    }
}
