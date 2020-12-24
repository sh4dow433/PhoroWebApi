using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess
{
    public class UserAuth : IUserAuth
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAuth(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Login(string username, string password)
        {
            var user = _unitOfWork.Users.GetByUsername(username);
            if (user == null)
                return null; // User does not exist.

            if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt) == false)
                return null;

            return user;
        }

        public bool Register(User user, string password)
        {

            if (CheckIfUserExists(user.Username) == true)
                return false;

            if (CheckIfEmailExists(user.Email) == true)
                return false;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
            return true;

        }

        private bool CheckIfUserExists(string username)
        {
            if (_unitOfWork.Users.GetByUsername(username) == null)
                return false;
            return true;
        }

        private bool CheckIfEmailExists(string emailAddress)
        {
            if (_unitOfWork.Users.GetByEmail(emailAddress) == null)
                return false;
            return true;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool UpdatePassword(User user, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
