using Microsoft.EntityFrameworkCore;
using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class AlbumsRepository : Repository<Album>, IAlbumsRepository
    {
        public AlbumsRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public override Album GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Albums                
                .Where(a => a.Id == id)
                .Include(a => a.AlbumPhotos)
                .FirstOrDefault();
        }

        public bool AddPhotoToAlbum(int albumId, Photo photo)
        {
            var album = _dbContext.Albums.Where(a => a.Id == albumId).FirstOrDefault();
            if (album == null)
            {
                return false;
            }
            if (_dbContext.AlbumsPhotos.Where(ap => ap.AlbumId == album.Id && ap.PhotoId == photo.Id).FirstOrDefault() is null == false)
            {
                return false;
            }

            album.AlbumPhotos.Add(new AlbumPhoto
            {
                Album = album,
                Photo = photo
            });
            return true;
        }

        public bool RemovePhotoFromAlbum(int albumId, Photo photo)
        {
            var album = _dbContext.Albums.Where(a => a.Id == albumId).FirstOrDefault();
            if (album == null)
            {
                return false;
            }
            var albumPhoto = _dbContext.AlbumsPhotos
                .Where(ap => ap.AlbumId == albumId && ap.Photo == photo)
                .FirstOrDefault();
            if (albumPhoto == null)
            {
                return false;
            }

            album.AlbumPhotos.Remove(albumPhoto);
            return true;
        }

        public IEnumerable<Album> GetAllByOwnersId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Albums
                .Where(a => a.SellerId == id)
                .ToList();

        }
    }
}