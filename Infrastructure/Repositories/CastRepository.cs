using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        //return a cast and information of his correspoding movies
        public override async Task<Cast> GetById(int castId)
        {
            var cast = await _dbContext.Casts.Include(c => c.MoviesOfCast).ThenInclude(c => c.Movie).Where(c => c.Id == castId).FirstOrDefaultAsync();
            return cast;
        }
    }
}
