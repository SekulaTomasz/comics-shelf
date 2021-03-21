using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Models
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int Coins { get; set; }

        public virtual ICollection<PurchasedComicsUsers> PurchasedComic { get; set; }

    }
}
