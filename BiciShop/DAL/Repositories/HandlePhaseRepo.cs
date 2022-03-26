using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HandlePhaseRepo : RepositoryBase<HandlePhase>
    {
        public HandlePhaseRepo(BiciContext context) : base(context)
        {

        }
    }
}
