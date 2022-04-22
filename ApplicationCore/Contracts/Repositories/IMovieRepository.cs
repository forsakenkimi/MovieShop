using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        //addtional methods specific to movie class besides CRUD methods
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();

        Task<IEnumerable<Movie>> Get30HighestRatedMovies();

        Task<PagedResultSet<Movie>> GetMoviesByGenres(int id, int pageSize = 30, int pageNumber = 1);

        Task<PagedResultSet<Movie>> GetMovies(int pageSize = 30, int pageNumber = 1);

        Task<PagedResultSet<Review>> GetReviewsByMovies(int id, int pageSize = 30, int pageNumber = 1);

    }

}
