using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Models
{
    public class PurchasedComicsUsers : Entity
    {
        public bool PurchasedAsExclusive { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Cost { get; set; }

        public User User { get; set; }
        public Comics Comics { get; set; }

    }
}
