﻿using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<MovieGenre> AddMovieAndGenreByAdmin(int movieId, int genreId)
        {
            MovieGenre movieGenre = new MovieGenre()
            {
                MovieId = movieId,
                GenreId = genreId,
            };
            await _dbContext.AddAsync<MovieGenre>(movieGenre);
            return movieGenre;

        }

        //public async Task<MovieCreateRequestModel> AddMovieByAdmin(MovieCreateRequestModel movie)
        //{
        //    Movie AddedMovie = new Movie()
        //    {
        //        Title = movie.Title,
        //        Overview = movie.Overview,
        //        Tagline = movie.Tagline,
        //        Budget = movie.Budget,
        //        Revenue = movie.Revenue,
        //        ImdbUrl = movie.ImdbUrl,
        //        TmdbUrl = movie.TmdbUrl,
        //        PosterUrl = movie.PosterUrl,
        //        BackdropUrl = movie.BackdropUrl,
        //        OriginalLanguage = movie.OriginalLanguage,
        //        ReleaseDate = movie.ReleaseDate,
        //        RunTime = movie.RunTime,
        //        Price = movie.Price,
        //    };
        //    await _dbContext.AddAsync<Movie>(AddedMovie);
        //    return movie;

        //}

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m=>m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            var GroupedMovieRatings = await _dbContext.Reviews.GroupBy(r => r.MovieId).
                Select(gr => new {MovieId = gr.Key, AvgRating = gr.Average(r => r.Rating) }).OrderByDescending(gmr => gmr.AvgRating).Take(30).ToListAsync();
            var movies = new List<Movie>();
            foreach (var MovieRating in GroupedMovieRatings)
            {
                Movie movie = await GetById(MovieRating.MovieId);
                movie.Rating = MovieRating.AvgRating;
                movies.Add(movie);
            }
            return movies;
        }

        public override async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast).Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            //include => join with junction table; theninclude => join with the other table in the junction table  
            //

            movie.Rating = await _dbContext.Reviews.Where(m => m.MovieId == id).AverageAsync(m =>m.Rating);
            return movie;
        }

        //public async Task<int> GetMovieIdByInfo(MovieCreateRequestModel movie)
        //{
            
                    //this method is supposed to be created for [post] new movie

        //}

        public async Task<PagedResultSet<Movie>> GetMovies(int pageSize = 30, int pageNumber = 1)
        {
            //Debug.WriteLine(_dbContext.MovieGenres.Where(mg => mg.GenreId == id).Count());
            var totalMoviesCount = await _dbContext.Movies.CountAsync();

            if (totalMoviesCount == 0)
            {
                throw new Exception("No Movies Found");
            }
            List<Movie> movies = await _dbContext.Movies.Select(mg => new Movie { Id = mg.Id, Title = mg.Title, PosterUrl = mg.PosterUrl })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            PagedResultSet<Movie> pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
            return pagedMovies;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenres(int id, int pageSize = 30, int pageNumber = 1)
        {
            //Debug.WriteLine(_dbContext.MovieGenres.Where(mg => mg.GenreId == id).Count());
            var totalMoviesCountByGenre = await _dbContext.MovieGenres.Where(mg => mg.GenreId == id).CountAsync();

            if (totalMoviesCountByGenre == 0)
            {
                throw new Exception("No Movies Found For That Genre");
            }
            List<Movie> movies = await _dbContext.MovieGenres.Where(mg => mg.GenreId == id).Include(mg => mg.Movie).Select(mg => new Movie {Id = mg.MovieId, Title= mg.Movie.Title, PosterUrl = mg.Movie.PosterUrl})
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            PagedResultSet<Movie> pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCountByGenre);
            return pagedMovies;
        }

        public async Task<PagedResultSet<Review>> GetReviewsByMovies(int id, int pageSize = 30, int pageNumber = 1)
        {
            //Debug.WriteLine(_dbContext.MovieGenres.Where(mg => mg.GenreId == id).Count());
            var totalReviewsCount = await _dbContext.Movies.CountAsync();

            if (totalReviewsCount == 0)
            {
                throw new Exception("No Movies Found");
            }
            List<Review> reviews = await _dbContext.Reviews.Where(r => r.MovieId == id).Select(r => new Review { MovieId = r.MovieId, UserId = r.UserId, Rating = r.Rating, ReviewText=r.ReviewText })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            PagedResultSet<Review> pagedReviews = new PagedResultSet<Review>(reviews, pageNumber, pageSize, totalReviewsCount);
            return pagedReviews;
        }
    }


}
