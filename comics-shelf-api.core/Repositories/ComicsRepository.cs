using comics_shelf_api.core.Database;
using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Models;
using comics_shelf_api.core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace comics_shelf_api.core.Repositories
{
    public class ComicsRepository : IComicsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ComicsRepository(DatabaseContext databaseContext) {
            this._databaseContext = databaseContext;
        }
        public async Task<Comics> CreateNewComics(Comics comics)
        {
            _databaseContext.Database.BeginTransaction();
            _databaseContext.Comics.Add(comics);
            await _databaseContext.SaveChangesAsync();
            _databaseContext.Database.CommitTransaction();
            return await _databaseContext.Comics.FirstOrDefaultAsync(x => x.DiamondId == comics.DiamondId); 
        }

        public async Task<Comics> GetComicsByExternalApiId(string diamondId) => await _databaseContext.Comics.FirstOrDefaultAsync(x => x.DiamondId == diamondId);

        public async Task<Comics> GetComicsById(Guid comicsId) => await _databaseContext.Comics.FirstOrDefaultAsync(x => x.Id == comicsId);
    }
}
