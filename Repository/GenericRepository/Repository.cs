using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected PumpDbContext ctx;

        public Repository(PumpDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T entity)
        {
            ctx.Set<T>().Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(string id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll() {
            return ctx.Set<T>();
        }

        public abstract T Read(string id);
        public abstract void Update(T entity);
    }
}
