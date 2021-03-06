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

        public async Task<Favorite> AddUserMovieFavorite(int userId, int movieId)
        {
            Favorite fav = new Favorite { 
                Id = userId,
                MovieId = movieId,
            };
            _dbContext.Set<Favorite>().Add(fav);
            await _dbContext.SaveChangesAsync();
            return fav;
        }

        public async Task RemoveMovieFavorite(int userId, int movieId)
        {
            var MovieFavorite = await _dbContext.Favorites.Where(m => m.MovieId == movieId).Where(m => m.UserId == userId).FirstOrDefaultAsync();
            if (MovieFavorite != null)
            {
                _dbContext.Set<Favorite>().Remove(MovieFavorite);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Favorite>> GetAllFavoritesForUser(int userId)
        {
            var favorites = await _dbContext.Favorites.Where(f => f.UserId == userId).ToListAsync();
            return favorites;
        }

        public async Task<List<MovieCardModel>> GetAllFavoritesMovieCardForUser(int userId)
        {
            var favoriteMovies = await _dbContext.Favorites.Include(f => f.Movie).ToListAsync();
            List<MovieCardModel> movieCards = new List<MovieCardModel>();
            foreach (var favoriteMovie in favoriteMovies)
            {
                movieCards.Add(new MovieCardModel { 
                    Id = favoriteMovie.Movie.Id,
                    Title = favoriteMovie.Movie.Title,
                    PosterUrl = favoriteMovie.Movie.PosterUrl,
                });
                
            };
            return movieCards;
        }
        public Task<Review> AddMovieReview(int movieId, int userId, decimal rating, string reviewText)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateMovieReview(int movieId, int userId, decimal rating, string reviewText)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            Review review = new Review()
            {
                MovieId = movieId,
                UserId = userId,

            };
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllReviewsByUser(int userId)
        {
            var reviews =  await _dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            Purchase purchase = new Purchase()
            {
                UserId = purchaseRequest.UserId,
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId,
            };
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<Favorite> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            Favorite favorite = new Favorite() { 
                UserId = favoriteRequest.UserId,
                MovieId =favoriteRequest.MovieId,
            };
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<Review> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            Review review = new Review()
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
            };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            Favorite favorite = new Favorite()
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId,
            };
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
            return true;
        }        

        public async Task<Review> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            Review review = new Review()
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
            };
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}
