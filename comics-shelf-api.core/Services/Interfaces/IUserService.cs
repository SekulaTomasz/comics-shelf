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
        Task<ApiResult<UserDto>> FindUserByIdAsync(Guid id);
        Task<ApiResult<UserDto>> FindUserByLoginAsync(string login);
        Task<ApiResult<UserDto>> RegisterUserAsync(UserLoginDto user);
        Task<ApiResult<UserDto>> LoginUserAsync(UserLoginDto user);
    }
}
