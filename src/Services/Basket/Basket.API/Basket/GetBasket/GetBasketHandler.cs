
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketReuslt>;
    public record GetBasketReuslt(ShoppingCart Cart);
    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketReuslt>
    {
        public async Task<GetBasketReuslt> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.UserName, cancellationToken);
            return new GetBasketReuslt(basket);
        }
    }
}
