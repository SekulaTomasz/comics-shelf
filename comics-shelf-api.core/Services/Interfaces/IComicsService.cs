using comics_shelf_api.core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Services.Interfaces
{
    public interface IComicsService
    {
        Task<ApiResult<PagedResult<ExternalProviderComicsDto>>> GetAvailableComicsAsync(int currentPage);
        Task<ApiResult<List<ExternalProviderComicsDto>>> GetComicsByTitle(string title);
    }
}
