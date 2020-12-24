using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class AdminsRepository : Repository<Admin>, IAdminsRepository 
    {
        public AdminsRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public Admin GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) == true)
            {
                throw new ArgumentException();
            }
            int userId = _dbContext.Users
                        .Where(u => u.Username == username)
                        .FirstOrDefault().Id;
            return _dbContext.Admins
                .Where(s => s.UserId == userId)
                .FirstOrDefault();
        }
    }
}
