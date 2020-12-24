using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface ISellersRepository : IRepository<Seller>
    {
        Seller GetByUsername(string username);
        Seller GetByUserId(int userId);
        IEnumerable<Seller> GetAllByMaxPrice(decimal maxPrice);
        List<Seller> GetAllByNameLike(string username);
        List<Seller> GetAllByLocationLike(string location);
    }
}
