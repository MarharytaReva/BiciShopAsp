using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class Cart
    {
        private List<CartLine> cartLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get => cartLines;
            set
            {
                cartLines = value;
            }
        }

        public void Add(BicicletaDTO bicicleta, int count)
        {
            if (!CartLines.Any(x => x.Bicicleta.BicicletaId == bicicleta.BicicletaId) && bicicleta.Quantity != 0)
            {
                CartLine newItem = new CartLine()
                {
                    Count = count,
                    Bicicleta = bicicleta
                };
                cartLines.Add(newItem);
            }  
        }
        public void Remove(BicicletaDTO bicicleta)
        {
            cartLines.RemoveAll(x => x.Bicicleta.BicicletaId == bicicleta.BicicletaId);
        }
        public void Clear()
        {
            cartLines.Clear();
        }
        public double GetTotalValue()
        {
            return cartLines.Sum(x => Convert.ToInt32((x.Bicicleta.Price * (100 - x.Bicicleta.Discount)) / 100) * x.Count);
        }
       
    }
}
