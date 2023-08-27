using ECommerce.Data.Abstracts;
using ECommerce.Data.Data;
using ECommerce.Entity.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Data.Concretes
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<bool> DeleteBasketItemAsync(string basketId,int id)
        {
            Basket basket = await GetBasketAsync(basketId);
            BasketItem bi = basket.items.Where(p => p.id == id).FirstOrDefault();
            basket.items.Remove(bi);
            return await _database.StringSetAsync(basketId, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
  
        }

        public async Task<Basket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var created = await _database.StringSetAsync(basket.id,
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.id);
        }
    }
}
