using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IPhotosRepository : IRepository<Photo>
    {
        IEnumerable<Photo> GetAllByOwnersId(int id);
        void RemoveSet(IEnumerable<Photo> photos);
    }
}
