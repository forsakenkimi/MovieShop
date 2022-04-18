using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<List<Purchase>> GetAllPurchasesForUser(int id);
        Task<PurchaseMovieDetailModel> GetPurchasesDetails(int userId, int movieId);
        Task<Favorite> AddUserMovieFavorite(int userId, int movieId);
        Task RemoveMovieFavorite(int userId, int movieId);
        Task <List<Favorite>> GetAllFavoritesForUser(int userId);
        Task<List<MovieCardModel>> GetAllFavoritesMovieCardForUser(int userId);

        Task<Review> AddMovieReview (int movieId, int userId, decimal rating, string reviewText);

        Task<Review> UpdateMovieReview(int movieId, int userId, decimal rating, string reviewText);

        Task<Review> DeleteMovieReview (int userId, int movieId);

        Task<Review> GetAllReviewsByUser(int userId);
    }
}
