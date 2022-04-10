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

        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            var movies = _dbContext.Movies.OrderByDescending(m=>m.Revenue).Take(30).ToList();
            return movies;
        }

        public IEnumerable<Movie> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }

        public override Movie GetById(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefault(m => m.Id == id);
            //include => join with junction table; theninclude => join with the other table in the junction table  

            movie.Rating = _dbContext.Reviews.Where(m => m.MovieId == id).Average(m =>m.Rating);
            return movie;
        }
    }


}
