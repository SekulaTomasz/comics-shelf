using comics_shelf_api.core.Database;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace comics_shelf_api.core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) {
            this._context = context;
        }

        public async Task<User> CreateUser(string login, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = login,
                PasswordHash = password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await this.FindUserByIdAsync(user.Id);
        }

        public async Task<User> FindUserByIdAsync(Guid id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

    }
}
