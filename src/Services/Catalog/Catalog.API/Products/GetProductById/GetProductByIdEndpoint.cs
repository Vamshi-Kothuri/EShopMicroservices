namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
              .WithName("GetProduct")
              .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithDescription("Get Products")
              .WithSummary("Get Products");
        }
    }
}
