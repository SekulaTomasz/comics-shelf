using comics_shelf_api.core.Repositories.Interfaces;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            this._userRepository = userRepository;
        }
    }
}
