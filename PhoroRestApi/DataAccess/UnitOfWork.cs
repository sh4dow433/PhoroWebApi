using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            Admins = new AdminsRepository(_dbContext);
            Albums = new AlbumsRepository(_dbContext);
            Messages = new MessagesRepository(_dbContext);
            Photos = new PhotosRepository(_dbContext);
            Sellers = new SellersRepository(_dbContext);
            Users = new UsersRepository(_dbContext);
            Reviews = new ReviewsRepository(_dbContext);
        }

        public IAdminsRepository Admins { get; private set; }
        public IAlbumsRepository Albums { get; private set; }
        public IMessagesRepository Messages { get; private set; }
        public IPhotosRepository Photos { get; private set; }
        public ISellersRepository Sellers { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IReviewsRepository Reviews { get; private set; }
       
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
