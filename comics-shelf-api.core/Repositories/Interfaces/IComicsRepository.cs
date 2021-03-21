using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Repositories.Interfaces
{
    public interface IComicsRepository
    {
        Task<Comics> GetComicsById(Guid comicsId);
        Task<Comics> GetComicsByExternalApiId(string diamondId);
        Task<Comics> CreateNewComics(Comics comics);
    }
}
