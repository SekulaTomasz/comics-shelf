using comics_shelf_api.core.Database;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace comics_shelf_api.core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) {
            this._context = context;
        }
        public async Task<User> FindUserByIdAsync(Guid id) {
            return await _context.Users.FindAsync(id);
        }
    }
}
