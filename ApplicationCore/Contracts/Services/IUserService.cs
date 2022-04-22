using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<List<int>> GetAllPurchasesMovieId(int userId);
        Task<IEnumerable<PurchaseMovieCardModel>> PurchaseMovie(int UserId);

        Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(int userId, int movieId);

        Task<Favorite> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(int userId, int movieId);

        Task<bool> FavoriteExists(int userId, int movieId);

        Task<List<MovieCardModel>> GetAllFavoritesMovieCard(int userId);

        Task<List<Review>> GetAllReviewsByUserId(int userId);

        Task<Review> AddMovieReview(ReviewRequestModel reviewRequest);

        Task<bool> DeleteMovieReview(int userId, int movieId);
        Task<Review> UpdateMovieReview(ReviewRequestModel reviewRequest);

        Task<List<Favorite>> GetAllFavoritesByUserId(int userId);

        Task<PurchaseMovieCardModel> PurchaseMovieByMovieId(int movieId, int userId);

        Task<bool> IsMovieFavored(int userId, int movieId);

    }
}
