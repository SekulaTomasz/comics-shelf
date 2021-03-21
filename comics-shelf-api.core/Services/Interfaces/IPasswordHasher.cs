using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string GetHash(string value);
        bool VerifyPassword(string hashPassword, string plainPassword);
    }
}
