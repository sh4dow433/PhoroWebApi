using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IUsersRepository : IRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
