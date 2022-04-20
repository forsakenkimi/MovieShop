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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<int>> GetAllPurchasesMovieId(int userId)
        {
            var purchases = await _userRepository.GetAllPurchasesForUser(userId);
            var MovieIdList = new List<int>();
            foreach (var purchase in purchases)
            {
                MovieIdList.Add(purchase.MovieId);
            }
            return MovieIdList;
        }
        public async Task<IEnumerable<PurchaseMovieCardModel>> PurchaseMovie(int userId)
        {
            var purchaseMovieCard = new List<PurchaseMovieCardModel>();
            var purchases = await _userRepository.GetAllPurchasesForUser(userId);
            foreach (var purchase in purchases)
            {
                var purchaseMovieDetail = await _userRepository.GetPurchasesDetails(userId, purchase.MovieId);
                purchaseMovieCard.Add(new PurchaseMovieCardModel
                {
                    movieCard = new MovieCardModel
                    {
                        Id = purchase.MovieId,
                        Title = purchaseMovieDetail.Title,
                        PosterUrl = purchaseMovieDetail.PosterUrl,                       
                    },
                    purchaseId = purchase.Id,
                    UserId = userId,
                    PurchaseNumber = purchase.PurchaseNumber,
                    TotalPrice = purchase.TotalPrice,
                    PurchaseDateTime = purchase.PurchaseDateTime,

                });
            }
            return purchaseMovieCard;
        }
        public async Task<bool> IsMoviePurchased(int userId, int movieId)
        {
            var purchases = await _userRepository.GetAllPurchasesForUser(userId);
            foreach (var purchase in purchases)
            {
                if (purchase.Id == movieId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Favorite> AddFavorite(int userId, int movieId)
        {
            var fav = await _userRepository.AddUserMovieFavorite(userId, movieId);
            return fav;
        }

        public async Task RemoveFavorite(int userId, int movieId)
        {
            await _userRepository.RemoveMovieFavorite(userId, movieId);
        }

        public async Task<bool> FavoriteExists(int userId, int movieId)
        {
            var favorites = await _userRepository.GetAllFavoritesForUser(userId);
            foreach (var favorite in favorites)
            {
                if (favorite.MovieId == movieId)
                {
                    return true;
                }
            }
            return false;
        }

        public Task<List<MovieCardModel>> GetAllFavoritesMovieCard(int userId)
        {
            var movieCards = _userRepository.GetAllFavoritesMovieCardForUser(userId);
            return movieCards;
        }

        public Task<List<Review>> GetAllReviewsByUserId(int userId)
        {
            return _userRepository.GetAllReviewsByUser(userId);
        }

        public async Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
           var purchase = await _userRepository.PurchaseMovie(purchaseRequest, userId);

            return purchase;
        }

        public async Task<Favorite> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _userRepository.AddFavorite(favoriteRequest);
            return favorite;
        }

        public async Task<Review> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _userRepository.AddMovieReview(reviewRequest);
            return review;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            return await _userRepository.RemoveFavorite(favoriteRequest);
            
        }

        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            return await _userRepository.DeleteMovieReview(userId, movieId);
        }

        public async Task<Review> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            return await _userRepository.UpdateMovieReview(reviewRequest);
        }

        public async Task<List<Favorite>> GetAllFavoritesByUserId(int userId)
        {
            return await _userRepository.GetAllFavoritesForUser(userId);
        }
    }
}
