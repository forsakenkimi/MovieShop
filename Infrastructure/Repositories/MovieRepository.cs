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
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m=>m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }

        public override async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            //include => join with junction table; theninclude => join with the other table in the junction table  
            //

            movie.Rating = await _dbContext.Reviews.Where(m => m.MovieId == id).AverageAsync(m =>m.Rating);
            return movie;
        }
    }


}
