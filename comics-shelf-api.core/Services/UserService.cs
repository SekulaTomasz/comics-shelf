using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Repositories.Interfaces;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            this._userRepository = userRepository;
        }

        public async Task<ApiResult<User>> FindUserByIdAsync(Guid id)
        {
            ApiResult<User> apiResult = new ApiResult<User>();
            try {
                var result = await _userRepository.FindUserByIdAsync(id);
                apiResult.Result = result;
            }
            catch (Exception ex) {
                apiResult.Error = ex.Message;
                apiResult.StatusCode = HttpStatusCode.BadRequest;
                return apiResult;
            }
            return apiResult;
        }
    }
}
