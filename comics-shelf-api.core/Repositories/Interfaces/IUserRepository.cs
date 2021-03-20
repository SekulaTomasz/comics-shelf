using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(Guid id);
    }
}
