using MediatR;
using QualificationTask.Domain.Entities;
using QualificationTask.Infrastructure.Persistance;

namespace QualificationTask.Application.Features.Products.Commands.AddProduct
{
    public sealed record AddProductCommand(AddProductRequest ProductRequest) : IRequest<int>;

    internal sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly QualificationTaskDbContext _dbContext;

        public AddProductCommandHandler(QualificationTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productRequest = request.ProductRequest;

            var product = new Product
            {
                ManufacturerCode = productRequest.ManufacturerCode,
                Name = productRequest.Name,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
