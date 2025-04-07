using Ecommerce.Apis.Errors;
using Ecommerce.Core.Entites;
using Ecommerce.Core.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apis.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController( IBasketRepository basketRepository )
        {
            _basketRepository = basketRepository;
        }

        #region GetBasket Or RecreateBasket

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket( string basketId )
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if ( basket is null ) return new CustomerBasket(basketId); // Recreate and return empty basket if not found

            return Ok(basket);
        }

        #endregion GetBasket Or RecreateBasket

        #region UpdateBasket Or CreateNewBasket

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket( CustomerBasket basket )
        {
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if ( CreatedOrUpdatedBasket is null ) return BadRequest(new ApiResponse(400, "Problem with your basket"));
            return Ok(CreatedOrUpdatedBasket);
        }

        #endregion UpdateBasket Or CreateNewBasket

        #region DeleteBasket

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket( string basketId )
        {
            var deleted = await _basketRepository.DeleteBasketAsync(basketId);
            if ( !deleted ) return BadRequest(new ApiResponse(400, $"No Basket with Id :{basketId} "));
            return Ok(deleted);
        }

        #endregion DeleteBasket
    }
}