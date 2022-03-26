using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> table;
        public RepositoryBase(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public T Create(T item)
        {
            table.Add(item);
           
            return item;
        }

        public T Delete(int id, int secondId = 0)
        {
            T entity = table.Find(id);
            if(entity != null)
            {
                table.Remove(entity);
            }
            return entity;
        }

        public virtual IQueryable<T> GetAll()
        {
            return table;
        }

        public virtual T GetItem(int id, int secondId = 0)
        {
            T entity = table.Find(id);
            return entity;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
