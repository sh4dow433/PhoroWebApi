using Microsoft.EntityFrameworkCore;
using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public User GetByUsername(string username)
        {
            //if(string.IsNullOrWhiteSpace(username) == true)
            //{
            //    throw new ArgumentException();
            //}
            return _dbContext.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();
        }

        

        public User GetByEmail(string email)
        {
            //if (string.IsNullOrWhiteSpace(email) == true)
            //{
            //    throw new ArgumentException();
            //}
            return _dbContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }
    }
}
