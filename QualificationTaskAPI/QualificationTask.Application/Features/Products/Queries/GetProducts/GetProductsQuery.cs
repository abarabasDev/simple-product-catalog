using MediatR;
using Microsoft.EntityFrameworkCore;
using QualificationTask.Infrastructure.Persistance;

namespace QualificationTask.Application.Features.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery() : IRequest<IEnumerable<GetProductsResponse>>;

    internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
    {
        private readonly QualificationTaskDbContext _dbContext;

        public GetProductsQueryHandler(QualificationTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);

            return products.Select(x => new GetProductsResponse
            {
                Id = x.Id,
                ManufacturerCode = x.ManufacturerCode,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
            });
        }
    }
}
