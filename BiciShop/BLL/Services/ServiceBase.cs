using AutoMapper;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceBase<T, D> : IService<D> where D : class where T : class
    {
        protected IRepository<T> repo;
        protected IMapper mapper;
        public ServiceBase(IRepository<T> repo)
        {
            this.repo = repo;
        }
        
        public D Create(D item)
        {
            T entity = mapper.Map<D, T>(item);
            repo.Create(entity);
            repo.Save();
            return item;
        }

        public async Task<D> CreateAsync(D item)
        {
            return await Task.Run(() =>
            {
                return Create(item);
            });
        }

        public D Delete(int id)
        {
            T entity = repo.Delete(id);
            repo.Save();
            if (entity is null)
                return null;
            return mapper.Map<T, D>(entity);
        }

        public async Task<D> DeleteAsync(int id)
        {
            return await Task.Run(() =>
            {
                return Delete(id);
            });
        }

        public D Update(D item)
        {
            T entity = mapper.Map<D, T>(item);
            repo.Update(entity);
            repo.Save();
            return item;
        }

        public async Task<D> UpdateAsync(D item)
        {
            return await Task.Run(() =>
            {
                return Update(item);
            });
        }

        public async Task<D> GetItemAsync(int id, int idSecond = 0)
        {
            return await Task.Run(() =>
            {
                T entity = repo.GetItem(id, idSecond);
                if (entity is null)
                    return null;
                D item = mapper.Map<T, D>(entity);
                return item;
            });
           
        }

        public async Task<IEnumerable<D>> GetAllAsync(int offset, int pageNumber)
        {
            return await Task.Run(() =>
            {
                var res = repo.GetAll().Skip((pageNumber - 1) * offset).Take(offset);
                var converted = mapper.Map<IEnumerable<T>, IEnumerable<D>>(res.ToList());
                return converted;
            });
        }
    }
}
