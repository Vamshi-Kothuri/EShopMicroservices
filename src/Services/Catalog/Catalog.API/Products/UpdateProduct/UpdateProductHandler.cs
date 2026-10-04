namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageFile,
        List<string> Categories): ICommand<UpdateProductResult>;
    public record UpdateProductResult(Product Product);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage("Product price should be Greater than Zero");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand query, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                                .Where(p => p.Id == query.Id)
                                .FirstOrDefaultAsync(cancellationToken);
            if (product == null) throw new ProductNotFoundException(query.Id);
            product.Name = query.Name;
            product.Description = query.Description;
            product.Price = query.Price;
            product.ImageFile = query.ImageFile;
            product.Categories = query.Categories;

           await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(product);
        }
    }
}
