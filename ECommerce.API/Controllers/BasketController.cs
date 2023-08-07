using ECommerce.Data.Abstracts;
using ECommerce.Data.Response;
using ECommerce.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BuggyController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new Entity.Entities.Basket(id));
        }
        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket([FromBody]Basket basket)
        {
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
           bool result = await _basketRepository.DeleteBasketAsync(id);

            if (result) return Ok(new ApiResponse(200,"Basket successfully deleted!"));

            return GetBadRequest();
        }
    }
}
