using AutoMapper;
using BiciShop.Models;
using BLL.DTO;
using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderUnitService : ServiceBase<OrderUnit, OrderUnitDTO>
    {
        public OrderUnitService(IRepository<OrderUnit> repo) : base(repo)
        {
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Bicicleta, BicicletaDTO>()
              .ForMember(y => y.QuantityStatus, y => y.MapFrom(source => StoreStatusManager.GetStoreStatus(source.Quantity)));

                x.CreateMap<BiciType, BiciTypeDTO>().ReverseMap();

                x.CreateMap<BicicletaDTO, Bicicleta>()
                .ForMember(y => y.BiciTypeId, y => y.MapFrom(source => source.BiciType.BiciTypeId));

                x.CreateMap<OrderUnit, OrderUnitDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
    }
}
