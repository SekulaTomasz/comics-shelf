using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class ExternalProviderComicsDto
    {
        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
        [JsonProperty(PropertyName = "creators")]
        public string Creators { get; set; }
        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty(PropertyName = "diamond_id")]
        public string DiamondId { get; set; }
    }

    public class ComicsProviderResult
    {
        [JsonProperty(PropertyName = "comics")]
        public List<ExternalProviderComicsDto> Comics { get; set; }
    }
}
