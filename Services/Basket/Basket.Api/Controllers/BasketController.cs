using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region constructor

        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountService)
        {
            _basketRepository = basketRepository;
            _discountService = discountService;
        }

        #endregion

        #region get basket

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        #endregion

        #region update basket

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // todo: get data from discount.grpc and calculate final price of product

            foreach (var item in basket.Items)
            {
                var copun = await _discountService.GetDiscount(item.ProductName);
                item.Price -= copun.Amount;
            }

            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        #endregion

        #region remove basket

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }

        #endregion
    }
}
