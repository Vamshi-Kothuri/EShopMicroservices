
namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken token = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, token);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart>  StoreBasket(ShoppingCart basket, CancellationToken token = default)
        {
            session.Store<ShoppingCart>(basket);
            await session.SaveChangesAsync(token);
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken token = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(token);

            return true;
        }


    }
}
