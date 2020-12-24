using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class ReviewsRepository : Repository<Review>, IReviewsRepository
    {
        public ReviewsRepository(AppDbContext dbContext) : base(dbContext) 
        {

        }
        public override IEnumerable<Review> GetAll()
        {
            return _dbContext.Reviews
                .OrderByDescending(r => r.CreationDate)
                .ToList();
        }
    }
}
