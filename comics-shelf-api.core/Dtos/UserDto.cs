using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class UserDto
    {
        public string Login { get; set; }
        public Guid Id { get; set; }
        public int Coins { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
