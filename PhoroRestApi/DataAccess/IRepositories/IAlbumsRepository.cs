using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IAlbumsRepository : IRepository<Album>
    {
        IEnumerable<Album> GetAllByOwnersId(int id);
        bool AddPhotoToAlbum(int albumId, Photo photo);
        bool RemovePhotoFromAlbum(int albumId, Photo photo);
    }
}
