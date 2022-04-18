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

        
    }
}
