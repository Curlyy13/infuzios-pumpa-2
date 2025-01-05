using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
