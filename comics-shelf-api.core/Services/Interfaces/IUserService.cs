using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResult<User>> FindUserByIdAsync(Guid id);
    }
}
