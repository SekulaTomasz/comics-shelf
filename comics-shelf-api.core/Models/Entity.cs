using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAd { get; set; }
    }
}
