using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IAdminsRepository : IRepository<Admin>
    {
        Admin GetByUsername(string username);

    }
}
