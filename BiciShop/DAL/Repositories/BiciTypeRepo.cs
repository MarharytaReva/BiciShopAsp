using BiciShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BiciTypeRepo : RepositoryBase<BiciType>
    {
        public BiciTypeRepo(BiciContext context) : base(context)
        {

        }
        public override BiciType Update(BiciType item)
        {
            BiciType entity = table.FirstOrDefault(x => x.BiciTypeId == item.BiciTypeId);
            entity.BiciTypeName = item.BiciTypeName;
            entity.BiciTypeId = item.BiciTypeId;

            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return item;
        }
    }
}
