using ECommerce.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Entities
{
    public class Basket
    {

        public Basket()
        {
            
        }

        public Basket(string id)
        {
            this.id = id;
        }

        public string id { get; set; }
        public List<BasketItem> items { get; set; } = new List<BasketItem>();
    }
}
