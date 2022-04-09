using ApplicationCore.Entities;
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
        IEnumerable<Movie> Get30HighestGrossingMovies();

        IEnumerable<Movie> Get30HighestRatedMovies();
    }
}
