using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
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
    }
}
