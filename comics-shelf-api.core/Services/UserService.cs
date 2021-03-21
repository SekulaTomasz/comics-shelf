using comics_shelf_api.core.Consts;
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
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) {
            this._userRepository = userRepository;
            this._passwordHasher = passwordHasher;
        }

        public async Task<ApiResult<UserDto>> FindUserByIdAsync(Guid id)
        {
            ApiResult<UserDto> apiResult = new ApiResult<UserDto>();
            try {
                var result = await _userRepository.FindUserByIdAsync(id);
                if (result == null)
                {
                    throw new Exception(String.Format(UserServiceError.USER_NOT_FOUND, id));

                }
                apiResult.Result = new UserDto()
                {
                    Coins = result.Coins,
                    CreatedAt = result.CreatedAt,
                    Id = result.Id,
                    Login = result.Login,
                    UpdatedAt = result.UpdatedAt
                };
            }
            catch (Exception ex) {
                apiResult.Error = ex.Message;
                apiResult.StatusCode = HttpStatusCode.BadRequest;
                return apiResult;
            }
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> FindUserByLoginAsync(string login)
        {
            ApiResult<UserDto> apiResult = new ApiResult<UserDto>();
            try
            {
                var result = await _userRepository.FindUserByLoginAsync(login);
                apiResult.Result = new UserDto()
                {
                    Coins = result.Coins,
                    CreatedAt = result.CreatedAt,
                    Id = result.Id,
                    Login = result.Login,
                    UpdatedAt = result.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                apiResult.Error = ex.Message;
                apiResult.StatusCode = HttpStatusCode.BadRequest;
                return apiResult;
            }
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> LoginUserAsync(UserLoginDto user)
        {
            
            var userExist = await _userRepository.FindUserByLoginAsync(user.Login);
            if (userExist == null) {
                return await this.RegisterUserAsync(user);
            }

            var passwordAreEqual = _passwordHasher.VerifyPassword(userExist.PasswordHash, user.Password);
            if (!passwordAreEqual) return new ApiResult<UserDto>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Error = LoginError.PASSWORD_INVALID
            };

            return new ApiResult<UserDto>()
            {
                StatusCode = HttpStatusCode.OK,
                Result = new UserDto() { 
                    UpdatedAt = userExist.UpdatedAt,
                    Login = userExist.Login,
                    Id = userExist.Id,
                    CreatedAt = userExist.CreatedAt,
                    Coins = userExist.Coins
                }
            };
        }

        public async Task<ApiResult<UserDto>> RegisterUserAsync(UserLoginDto user)
        {
            try {
                var hashPassword = _passwordHasher.GetHash(user.Password);
                var result = await _userRepository.CreateUser(user.Login, hashPassword);
                return new ApiResult<UserDto>()
                {
                    StatusCode = HttpStatusCode.Created,
                    Result = new UserDto()
                    {
                        Coins = result.Coins,
                        CreatedAt = result.CreatedAt,
                        Id = result.Id,
                        Login = result.Login,
                        UpdatedAt = result.UpdatedAt
                    }
                };
            } catch (Exception ex) {
                return new ApiResult<UserDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = ex.Message
                };
            }           
        }
    }
}
