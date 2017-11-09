using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Shop.Models
{
    public class ShopCartData
    {
        public ShopCartData()
        {
            Items = new BlockingCollection<Item>();
        }

        public int Price { get; set; }

        public BlockingCollection<Item> Items { get; set; }

        public Task<int> GetResultAsync()
        {
            var task = Task.Run(() =>
            {
                Price = 0;// reset prise
                foreach (var item in Items)
                {
                    Price += item.ItemPrice;
                }
                return Price;
            });

            return task;
        }
    }
}
