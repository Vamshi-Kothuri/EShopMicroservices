using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketCommand(string UserName);
    public record DeleteBasketResponse(bool IsDeleted);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(UserName));
                var reponse = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(result);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");
        }
    }
}
