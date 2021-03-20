using comics_shelf_api.core.Consts;
using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.ExternalProviders
{
    public class ComicsProvider : IComicsProvider
    {
        private readonly IHttpClientFactory _clientFactory;

        public ComicsProvider(IHttpClientFactory clientFactory) {
            this._clientFactory = clientFactory;
        }

        public async Task<List<ExternalProviderComicsDto>> GetComicsFromProvider() {
            try {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.shortboxed.com/comics/v1/new");

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(ComicsProviderError.CANNOT_FETCH_COMICS);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ComicsProviderResult>(content).Comics;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
