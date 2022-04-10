﻿using ApplicationCore.Contracts.Repositories;
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

        public List<MovieCardModel> Get30HighestGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();
            //AutoMapper
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel
                {
                    Title = movie.Title,
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                });
            }
            return movieCards;
        }

         public MovieDetailModel GetMovieDetails(int id)
        {
            var movie = _movieRepository.GetById(id);

            var movieDetails = new MovieDetailModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Rating = movie.Rating,
                
                
            };

            movieDetails.Trailers = new List<TrailerModel>();
            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerModel
                {
                    Id = trailer.Id,
                    TrailerUrl = trailer.TrailerUrl,
                    Name = trailer.Name,
                });
            }

            movieDetails.Casts = new List<CastModel>();
            foreach (var cast in movie.CastsOfMovie)
            {
                movieDetails.Casts.Add(new CastModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    ProfilePath = cast.Cast.ProfilePath,
                    Character = cast.Character,
                });
            }

            return movieDetails;

            movieDetails.Genres = new List<GenreModel>();

            foreach (var genre in movie.GenresOfMovie)
            {
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = genre.GenreId,
                    Name = genre.Genre.Name,
                });
            }
            

        }
    }
}
