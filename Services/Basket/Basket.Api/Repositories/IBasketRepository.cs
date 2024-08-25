using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetUserBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
