using AutoMapper;
using BiciShop.Models;
using BLL.DTO;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BiciTypeService : ServiceBase<BiciType, BiciTypeDTO>
    {
        public BiciTypeService(IRepository<BiciType> repository) : base(repository)
        {
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<BiciType, BiciTypeDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        public async Task<IEnumerable<BiciTypeDTO>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var collection = repo.GetAll();
                return mapper.Map<IEnumerable<BiciType>, IEnumerable<BiciTypeDTO>>(collection);
            });
        }
        public async Task<BiciTypeDTO> GetItemAsync(string name)
        {
            return await Task.Run(() =>
            {
                return GetItem(name);
            });
        }
        public BiciTypeDTO GetItem(string name)
        {
                var res = repo.GetAll().FirstOrDefault(x => x.BiciTypeName.ToLower() == name.ToLower());
                if (res is null)
                    return null;
                var converted = mapper.Map<BiciType, BiciTypeDTO>(res);
                return converted;
        }
        public async Task<IList<string>> GetAllNamesAsync()
        {
            return await Task.Run(() =>
            {
                var collection = repo.GetAll().Select(x => x.BiciTypeName);
                return collection.ToList();
            });
        }
    }
}
