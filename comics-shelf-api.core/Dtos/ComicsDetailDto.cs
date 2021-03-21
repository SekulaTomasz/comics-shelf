using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class ComicsDetailDto
    {
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
