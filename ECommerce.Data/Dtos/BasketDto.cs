using ECommerce.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Dtos
{
    public class BasketDto
    {
        public string id { get; set; }
        public List<BasketItemDto> items { get; set; } = new List<BasketItemDto>();
    }
}
