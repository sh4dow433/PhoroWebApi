using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
    }
}
