using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepo : RepositoryBase<Order>
    {
        public OrderRepo(BiciContext context) : base(context)
        {

        }
        public override IQueryable<Order> GetAll()
        {
            return table.Include(x => x.OrderUnits).ThenInclude(x => x.Bicicleta).ThenInclude(x => x.BiciType).Include(x => x.HandlePhase);
        }
        public override Order GetItem(int id, int secondId = 0)
        {
            return table.Include(x => x.OrderUnits).ThenInclude(x => x.Bicicleta).Include(x => x.HandlePhase).FirstOrDefault(x => x.OrderId == id);
        }
        public override Order Update(Order item)
        {
            Order entity = table.FirstOrDefault(x => x.OrderId == item.OrderId);
            entity.HandlePhaseId = item.HandlePhaseId;

            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return item;
        }
    }
}
