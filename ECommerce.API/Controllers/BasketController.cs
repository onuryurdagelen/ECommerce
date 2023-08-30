using AutoMapper;
using ECommerce.Data.Abstracts;
using ECommerce.Data.Dtos;
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
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            Basket? basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new Entity.Entities.Basket(id));
        }
        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket([FromBody]BasketDto basketDto)
        {
            Basket basket = _mapper.Map<BasketDto, Basket>(basketDto);
            Basket updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string basketId)
        {
           bool result = await _basketRepository.DeleteBasketAsync(basketId);

            if (result) return Ok(new ApiResponse(200,"Basket successfully deleted!"));

            return GetBadRequest();
        }
        [HttpDelete("basketItem")]
        public async Task<IActionResult> DeleteBasketItemAsync([FromQuery]string basketId,[FromQuery]int id)
        {
            bool result = await _basketRepository.DeleteBasketItemAsync(basketId,id);

            BasketDto basketDto = _mapper.Map<Basket, BasketDto>(await _basketRepository.GetBasketAsync(basketId));

            if (result) return Ok(new ApiResponse<BasketDto>(true, "Basket item successfully deleted!", basketDto));

            return GetBadRequest();
        }
    }
}
