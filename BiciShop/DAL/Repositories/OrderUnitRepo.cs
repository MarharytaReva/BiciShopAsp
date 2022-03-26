using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderUnitRepo : RepositoryBase<OrderUnit>
    {
        public OrderUnitRepo(BiciContext context) : base(context)
        {

        }
        public override IQueryable<OrderUnit> GetAll()
        {
            return table.Include(x => x.Bicicleta);
        }
        public override OrderUnit GetItem(int id, int secondId = 0)
        {
            return table.Include(x => x.Bicicleta).FirstOrDefault(x => x.OrderId == id);
        }
    }
}
