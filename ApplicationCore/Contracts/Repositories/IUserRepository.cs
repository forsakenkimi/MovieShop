﻿using ApplicationCore.Entities;
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
    }
}
