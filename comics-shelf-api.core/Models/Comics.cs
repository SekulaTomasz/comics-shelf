using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Models
{
    public class Comics: Entity
    {
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Creators { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string DiamondId { get; set; }
    }
}
