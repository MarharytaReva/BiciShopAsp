using AutoMapper;
using BiciShop.Models;
using BLL.DTO;
using BLL.DTO.QuerySettings;
using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : ServiceBase<Order, OrderDTO>
    {
        public OrderService(IRepository<Order> repository) : base(repository)
        {
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Bicicleta, BicicletaDTO>()
             .ForMember(y => y.QuantityStatus, y => y.MapFrom(source => StoreStatusManager.GetStoreStatus(source.Quantity)));

                x.CreateMap<BiciType, BiciTypeDTO>().ReverseMap();
                x.CreateMap<HandlePhase, HandlePhaseDTO>().ReverseMap();

                x.CreateMap<BicicletaDTO, Bicicleta>()
                .ForMember(y => y.BiciTypeId, y => y.MapFrom(source => source.BiciType.BiciTypeId))
                .ForMember(y => y.BiciType, y => y.Ignore());

                x.CreateMap<OrderUnit, OrderUnitDTO>();

                x.CreateMap<OrderUnitDTO, OrderUnit>()
                .ForMember(y => y.Bicicleta, y => y.Ignore());

                x.CreateMap<Order, OrderDTO>();
                x.CreateMap<OrderDTO, Order>()
                .ForMember(y => y.HandlePhaseId, y => y.MapFrom(source => source.HandlePhase.HandlePhaseId))
                .ForMember(y => y.HandlePhase, y => y.Ignore());
            });
            mapper = new Mapper(configuration);
        }
        private IQueryable<Order> Filter(int phaseId)
        {
            if (phaseId == 0)
                return repo.GetAll();
            return repo.GetAll().Where(x => x.HandlePhaseId == phaseId);
        }
        private IQueryable<Order> Sort(OrderSort orderSort, IQueryable<Order> orders)
        {
            if (orderSort == OrderSort.Descreasing)
                return orders.OrderByDescending(x => x.Date);
            else
                return orders.OrderBy(x => x.Date);
        }
        public async Task<IEnumerable<OrderDTO>> GetAllAsync(int offset, string userId, OrderQueryOptions queryOptions)
        {
            return await Task.Run(() =>
            {
                var res = repo.GetAll().Where(x => x.UserKey.Equals(userId));
                res = Sort(queryOptions.OrderSort, res);
                res = res.Skip((queryOptions.PageNumber - 1) * offset).Take(offset);
                return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(res.ToList());
            });
        }
        public async Task<IEnumerable<OrderDTO>> GetAllAsync(int offset, OrderQueryOptions queryOptions)
        {
            return await Task.Run(() =>
            {
                var res = Filter(queryOptions.PhaseId);
                res = Sort(queryOptions.OrderSort, res);
                res = res.Skip((queryOptions.PageNumber - 1) * offset).Take(offset);
                var converted = res.ToList();
                return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(converted);
            });
        }
        public async Task<bool> IsDeletable(int biciId)
        {
            return await Task.Run(() =>
            {
                return !repo.GetAll().Any(x => x.OrderUnits.Any(x => x.BicicletaId == biciId) && !x.HandlePhase.IsCancelAvaliable);
            }); 
        }
        public async Task<int> GetUserOrdersCount(string userId, int offset)
        {
            return await Task.Run(() =>
            {
                int res = repo.GetAll().Where(x => x.UserKey.Equals(userId)).Select(x => x.OrderId).Count();
                int pageCount = res / offset;
                if (res % offset != 0)
                    pageCount++;
                return pageCount;
            });
        }
    }
}
