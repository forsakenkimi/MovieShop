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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        //Because of DI, our service doesnt know the existence of MovieRepository class. It doesnt even care and know how MovieRepository gets the movie data (using EF? Dapper? from SqlSever Oracle? => doesnt care)
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();
            //AutoMapper
            var movieCards = new List<MovieCard>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCard
                {
                    Title = movie.Title,
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                });
            }
            return movieCards;
        }
        
    }
}
