using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class PurchaseComicsDto
    {
        [JsonProperty(PropertyName = "comics")]
        public ExternalProviderComicsDto ExternalProviderComicsDto { get; set; }
        public Guid UserId { get; set; }
        public bool asExclusive { get; set; }
    }
}
