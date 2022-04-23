using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMovieRepository _movieRepository;
        public AdminService(IReportRepository reportRepository, IMovieRepository movieRepository)
        {
            _reportRepository = reportRepository;
            _movieRepository = movieRepository;
        }

        public async Task<MovieCreateRequestModel> AddMovieByAdmin(MovieCreateRequestModel movie)
        {
            Movie AddedMovie = new Movie()
            {
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
            };
            await _movieRepository.Add(AddedMovie);
            return movie;
        }

        //test
        public async Task<IEnumerable<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            return await _reportRepository.GetTopPurchasedMovies(fromDate, toDate, pageSize, pageIndex);
        }

        public async Task<MovieCreateRequestModel> UpdateMovieByAdmin(MovieCreateRequestModel movie)
        {
            Movie UpdateMovie = new Movie()
            {
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
            };
            await _movieRepository.Update(UpdateMovie);
            return movie;

        }
    }
}
