using Ecommerce.Core.Entites;
using Ecommerce.Core.Repository.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository( IConnectionMultiplexer redis ) // need to allow Dependency Injection
        {
            _database = redis.GetDatabase(); // get the database from the connection
        }

        public async Task<bool> DeleteBasketAsync( string basketId )
        {
            return await _database.KeyDeleteAsync(basketId);  // delete the basket from redis
        }

        public async Task<CustomerBasket?> GetBasketAsync( string basketId )
        {
            var Basket = await _database.StringGetAsync(basketId); // get the basket from redis

            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync( CustomerBasket basket )
        {
            var JsonBasket = JsonSerializer.Serialize(basket); // serialize the basket to json
            var CreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromDays(1)); // set the basket in redis with a expiration time of 1 day
            if ( !CreatedOrUpdated ) return null;
            return await GetBasketAsync(basket.Id); // get the basket from redis using our GetBasketAsync method
        }
    }
}