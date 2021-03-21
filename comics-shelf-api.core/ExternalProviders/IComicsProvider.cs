using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.ExternalProviders
{
    public interface IComicsProvider
    {
        Task<List<ExternalProviderComicsDto>> GetComicsFromProvider();
    }
}
