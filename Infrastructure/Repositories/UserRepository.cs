using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await  _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<List<Purchase>> GetAllPurchasesForUser(int id) {
            var purchases = await _dbContext.Purchases.Where(p => p.UserId == id).ToListAsync();
            return purchases;
        }

        public async Task<PurchaseMovieDetailModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchaseMovie = await _dbContext.Purchases.Include(m => m.Movie).Where(m => m.Movie.Id == movieId).FirstOrDefaultAsync();
            var puchaseMovieDetail = new PurchaseMovieDetailModel() {
                Id = purchaseMovie.Id,
                UserId = purchaseMovie.UserId,
                PurchaseNumber = purchaseMovie.PurchaseNumber,
                TotalPrice = purchaseMovie.TotalPrice,
                PurchaseDateTime = purchaseMovie.PurchaseDateTime,
                MovieId = purchaseMovie.Movie.Id,
                Title = purchaseMovie.Movie.Title,
                Overview = purchaseMovie.Movie.Overview,
                Tagline = purchaseMovie.Movie.Tagline,
                Budget = purchaseMovie.Movie.Budget,
                Revenue = purchaseMovie.Movie.Revenue,
                ImdbUrl = purchaseMovie.Movie.ImdbUrl,
                TmdbUrl = purchaseMovie.Movie.TmdbUrl,
                PosterUrl = purchaseMovie.Movie.PosterUrl,
                BackdropUrl = purchaseMovie.Movie.BackdropUrl,
                OriginalLanguage = purchaseMovie.Movie.OriginalLanguage,
                ReleaseDate = purchaseMovie.Movie.ReleaseDate,
                RunTime = purchaseMovie.Movie.RunTime,
                Price = purchaseMovie.Movie.Price,
            };
            return puchaseMovieDetail;
        }
    }
}
