using Microsoft.EntityFrameworkCore;
using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class SellersRepository : Repository<Seller>, ISellersRepository
    {
        public SellersRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public override Seller GetById(int id)
        {
            var result = _dbContext.Sellers
                .Where(s => s.Id == id)
                .Include(s => s.Reviews)
                .FirstOrDefault();
            return result;
        }

        public Seller GetByUserId(int userId)
        {
            var result = _dbContext.Sellers
                .Where(s => s.UserId == userId)
                .Include(s => s.Reviews)
                .FirstOrDefault();
            return result;
        }

        public IEnumerable<Seller> GetAllByMaxPrice(decimal maxPrice)
        {
            if (maxPrice <= 0)
            {
                throw new ArgumentException();
            }
            return _dbContext.Sellers
                .Where(s => s.Price <= maxPrice)
                .ToList();
        }

        public Seller GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) == true)
            {
                throw new ArgumentException();
            }
            int userId = _dbContext.Users
                        .Where(u => u.Username == username)
                        .FirstOrDefault().Id;
            return _dbContext.Sellers
                .Where(s => s.UserId == userId)
                .FirstOrDefault();
        }
        public List<Seller> GetAllByNameLike(string username)
        {
            List<int> ids = _dbContext.Users
                                .Where(u => u.Username.Contains(username))
                                .Select(u => u.Id)
                                .ToList();
            List<Seller> result = new List<Seller>();
            foreach(var id in ids)
            {
                var newRes = _dbContext.Sellers
                       .Where(s => s.UserId == id)
                       .Include(s => s.Reviews)
                       .FirstOrDefault();
                if (newRes == null)
                {
                    continue;
                }
                result.Add(newRes);
            }
            return result;
        }

        public List<Seller> GetAllByLocationLike(string location)
        {
            return _dbContext.Sellers
                .Where(s => s.Location.Contains(location))
                .Include(s => s.Reviews)
                .ToList();
        }
    }
}
