using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class PhotosRepository : Repository<Photo>, IPhotosRepository
    {
        public PhotosRepository(AppDbContext dbContext) : base(dbContext)
        {
                
        }

        public IEnumerable<Photo> GetAllByOwnersId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Photos
                .Where(p => p.UserId == id)
                .ToList();
        }

        public void RemoveSet(IEnumerable<Photo> photos)
        {
            if (photos == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Photos.RemoveRange(photos);
        }
    }
}
