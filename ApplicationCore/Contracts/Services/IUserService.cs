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
        Task<IEnumerable<PurchaseMovieCardModel>> PurchaseMovie(int UserId);

        Task<bool> IsMoviePurchased(int userId, int movieId);

        Task<Favorite> AddFavorite(int userId, int movieId);
        Task RemoveFavorite(int userId, int movieId);

        Task<bool> FavoriteExists(int userId, int movieId);

        Task<List<MovieCardModel>> GetAllFavoritesMovieCard(int userId);


    }
}
