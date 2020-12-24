using PhoroRestApi.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        public IAdminsRepository Admins { get;}
        public IAlbumsRepository Albums { get; }
        public IMessagesRepository Messages { get; }
        public IPhotosRepository Photos { get; }
        public ISellersRepository Sellers { get; }
        public IUsersRepository Users { get; }
        public IReviewsRepository Reviews { get; }


        int SaveChanges();
    }
}
