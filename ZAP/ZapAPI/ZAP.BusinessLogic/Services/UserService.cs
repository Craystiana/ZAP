using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZAP.BusinessLogic.DTO.User;
using ZAP.Common.Enums;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;

namespace ZAP.BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserModel> GetUserList()
        {
            var userList = _unitOfWork.UserRepository.GetAll();
            foreach (var user in userList)
            {
                yield return new UserModel
                {
                    Email = user.EmailAddress,
                    LastName = user.LastName,
                    FirstName = user.FirstName
                };
            }
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.SingleOrDefault(u => u.EmailAddress == email);
        }

        public bool Register(RegisterModel model)
        {
            var existingUser = _unitOfWork.UserRepository.SingleOrDefault(u => u.EmailAddress == model.Email);

            if (existingUser != null)
            {
                return false;
            }

            var salt = GenerateSalt();

            var newUser = new User
            {
                EmailAddress = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PasswordHash = CreatePasswordHash(model.Password, salt),
                Salt = salt,
                UserRoleId = 2
            };

            _unitOfWork.UserRepository.Add(newUser);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool RegisterAdmin(RegisterModel model)
        {
            var existingUser = _unitOfWork.UserRepository.SingleOrDefault(u => u.EmailAddress == model.Email);

            if (existingUser != null)
            {
                return false;
            }

            var salt = GenerateSalt();

            var newUser = new User
            {
                EmailAddress = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = CreatePasswordHash(model.Password, salt),
                Salt = salt,
                UserRoleId = (int)UserRoleType.Admin
            };

            _unitOfWork.UserRepository.Add(newUser);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool VerifyIdentity(string email, string password)
        {
            try
            {
                var user = GetUser(email);

                return user?.PasswordHash == CreatePasswordHash(password, user?.Salt);
            }
            catch
            {
                return false;
            }
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[50 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string CreatePasswordHash(string password, string salt)
        {
            using var sha1 = SHA1.Create();
            {
                var hashedBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public ProfileModel GetProfile(int userId)
        {
            var user = _unitOfWork.UserRepository.Get(userId);
            return new ProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.EmailAddress,
                Address = user.Address
            };
        }

        public void UpdateProfile(ProfileModel model, int userId)
        {
            var user = _unitOfWork.UserRepository.Get(userId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EmailAddress = model.Email;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            _unitOfWork.SaveChanges();
        }

        public int GetLoggedInUserId(string token)
        {
            return int.Parse(JwtService.GetClaim(TokenClaim.UserId, token));
        }

        public bool IsAdmin(string token)
        {
            var userRole = int.Parse(JwtService.GetClaim(TokenClaim.UserRole, token));

            return userRole == (int)UserRoleType.Admin;
        }
    }
}
