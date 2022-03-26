using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public enum StoreStatus
    {
        Available,
        NotAvailable,
        Few
    }
    public static class StoreStatusManager
    {
        public static int LowNumber { get; set; } = 5;
        public static StoreStatus GetStoreStatus(int quantity)
        {
            if (quantity == 0)
                return StoreStatus.NotAvailable;
            else if (quantity <= LowNumber)
                return StoreStatus.Few;

            return StoreStatus.Available;
        }
    }
}
