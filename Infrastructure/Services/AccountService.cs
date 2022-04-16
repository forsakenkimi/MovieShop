using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(RegisterModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null) {
                throw new Exception("Duplicated Email, please login");
            }else
            {
                var salt = GetRandomSalt();
                var hashedPassword = GetHashedPassword(model.Password, salt);
                var newUser = new User { 
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Salt = salt,
                    DateOfBirth = model.DateOfBirth,
                    HashedPassword = hashedPassword,
                };
                await _userRepository.Add(newUser);
            }
            return true;
        }

        public async Task<UserInfoModel> ValidateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Email Does Not Exsit");
            }
            var hashPassword = GetHashedPassword(password, user.Salt);
            if (hashPassword == user.HashedPassword)
            {
                var userInfo = new UserInfoModel() { 
                    Email = user.Email,
                    Id = user.Id,  
                    FirstName = user.FirstName,
                    LastName= user.LastName,
                    DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                };
            }
            throw new Exception("Email/Password does not match");
        }

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password,
           Convert.FromBase64String(salt),
           KeyDerivationPrf.HMACSHA512,
           10000,
           256 / 8));
            return hashed;
        }
    }
}
