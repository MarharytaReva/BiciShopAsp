using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IService<D> where D : class 
    {
        D Delete(int id);
        Task<D> DeleteAsync(int id);
        D Create(D item);
        Task<D> CreateAsync(D item);
        D Update(D item);
        Task<D> UpdateAsync(D item);
        Task<D> GetItemAsync(int id, int idSecond = 0);
        Task<IEnumerable<D>> GetAllAsync(int offset, int pageNumber);

    }
}
