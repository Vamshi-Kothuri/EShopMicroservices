
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        string Description,
        decimal Price,
        string ImageFile,
        List<string> Categories
    );

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                //Convert from Request to Command 
                var command = request.Adapt<CreateProductCommand>();
                //Send the Command to Command Handler and await the result
                var result = await sender.Send(command);
                //return result from CommandHandler -> response
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);

            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
