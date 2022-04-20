using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailModel> GetCastDetails(int castId)
        {
            var cast = await _castRepository.GetById(castId);
            string a = cast.Name;
            var castDetails = new CastDetailModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath
            };
            castDetails.Movies = new List<MovieModel>();
            foreach (var movie in cast.MoviesOfCast)
            {
                castDetails.Movies.Add(new MovieModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    Overview = movie.Movie.Overview,
                    Tagline = movie.Movie.Tagline,
                    Budget = movie.Movie.Budget,
                    Revenue = movie.Movie.Revenue,
                    ImdbUrl = movie.Movie.ImdbUrl,
                    TmdbUrl = movie.Movie.TmdbUrl,
                    PosterUrl = movie.Movie.PosterUrl,
                    BackdropUrl = movie.Movie.BackdropUrl,
                    OriginalLanguage = movie.Movie.OriginalLanguage,
                    ReleaseDate = movie.Movie.ReleaseDate,
                    RunTime = movie.Movie.RunTime,
                    Price = movie.Movie.Price,
                });
            }

            return castDetails;
        }
    }
}
