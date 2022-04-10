using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        List<MovieCardModel> Get30HighestGrossingMovies();
        MovieDetailModel GetMovieDetails(int id);
    }
}
