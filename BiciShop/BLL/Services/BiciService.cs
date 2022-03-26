using AutoMapper;
using BiciShop.Models;
using BLL.DTO;
using BLL.DTO.QuerySettings;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BiciService : ServiceBase<Bicicleta, BicicletaDTO>
    {
        public static string DefaultStr => "all";
        public BiciService(IRepository<Bicicleta> repository) : base(repository)
        {
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Bicicleta, BicicletaDTO>()
                .ForMember(y => y.QuantityStatus, y => y.MapFrom(source => StoreStatusManager.GetStoreStatus(source.Quantity)));

                x.CreateMap<BiciType, BiciTypeDTO>().ReverseMap();

                x.CreateMap<BicicletaDTO, Bicicleta>()
                .ForMember(y => y.BiciTypeId, y => y.MapFrom(source => source.BiciType.BiciTypeId))
                .ForMember(y => y.BiciType, y => y.Ignore());
            });
            mapper = new Mapper(configuration);
        }
        public async Task<IList<string>> GetAllColorsAsync()
        {
            return await Task.Run(() =>
            {
                var res = repo.GetAll().Select(x => x.Color.ToLower()).Distinct();
                return res.ToList();
            });
        }
        public async Task<int> GetPageCount(int offset, QueryOptions queryOptions)
        {
            return await Task.Run(() =>
            {
                var res = Search(queryOptions.SearchText);
                res = Filter(queryOptions.FiltrationOptions, res);
                res = Sort(queryOptions.SortOptions, res);
                int count = res.Count();

                int pageCount = count / offset;
                if (count % offset != 0)
                    pageCount++;
                return pageCount;
            });
        }

        public async Task<IEnumerable<BicicletaDTO>> GetAllAsync(int offset, QueryOptions queryOptions)
        {
            return await Task.Run(() =>
            {
                var res = Search(queryOptions.SearchText);
                res = Filter(queryOptions.FiltrationOptions, res);
                res = Sort(queryOptions.SortOptions, res);
                res = res.Skip((queryOptions.PageNumber - 1) * offset).Take(offset);
                var converted = res.ToList();
                return mapper.Map<IEnumerable<Bicicleta>, IEnumerable<BicicletaDTO>>(converted);
            });
        }
        public IQueryable<Bicicleta> Filter(FiltrationOptions filtrationOptions, IQueryable<Bicicleta> bicicletas)
        {
            if (filtrationOptions.Color != DefaultStr)
                bicicletas = bicicletas.Where(x => x.Color.ToLower() == filtrationOptions.Color.ToLower() || x.Color.ToLower().Contains(filtrationOptions.Color.ToLower()));
            if (filtrationOptions.BiciType != DefaultStr)
                bicicletas = bicicletas.Where(x => x.BiciType.BiciTypeName.ToLower() == filtrationOptions.BiciType.ToLower());
            return bicicletas;
        }
        public IQueryable<Bicicleta> Sort(SortOptions sortOptions, IQueryable<Bicicleta> bicicletas)
        {
            string propName;
            if (sortOptions.FieldSort == FieldSort.None)
                propName = "BicicletaId";
            else
                propName = Enum.GetName(typeof(FieldSort), sortOptions.FieldSort);

            PropertyInfo propertyInfo = typeof(Bicicleta).GetProperty(propName);

            var param = Expression.Parameter(typeof(Bicicleta));
            var expr = Expression.Lambda<Func<Bicicleta, object>>(
                Expression.Convert(Expression.Property(param, propertyInfo), typeof(object)),
                param
            );
            if (sortOptions.OrderSort == OrderSort.Increasing)
                return bicicletas.OrderBy(expr);
            else
                return bicicletas.OrderByDescending(expr);
        }
        public IQueryable<Bicicleta> Search(string searchText)
        {
            var bicicletas = repo.GetAll();
            if (!string.IsNullOrEmpty(searchText))
                bicicletas = bicicletas.Where(x => x.Title.ToLower().Contains(searchText.ToLower()));

            return bicicletas;
        }
        public void CreateOrUpdate(BicicletaDTO bicicleta)
        {
            Bicicleta entity = mapper.Map<BicicletaDTO, Bicicleta>(bicicleta);
            if (bicicleta.BicicletaId == 0)
            {
                repo.Create(entity);
            }
            else
            {
                ((BiciRepo)repo).Update(entity);
            }
            repo.Save();
        }
        public async Task<bool> IsTypeDeletable(int id)
        {
            return await Task.Run(() =>
            {
                return !repo.GetAll().Any(x => x.BiciTypeId == id);
            });
        }
    }
}
