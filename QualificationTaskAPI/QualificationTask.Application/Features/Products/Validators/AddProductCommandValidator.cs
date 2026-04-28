using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QualificationTask.Application.Features.Products.Commands.AddProduct;
using QualificationTask.Infrastructure.Persistance;

namespace QualificationTask.Application.Features.Products.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        private readonly QualificationTaskDbContext _dbContext;

        public AddProductCommandValidator(QualificationTaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.ProductRequest.ManufacturerCode)
                .MustAsync(BeUniqueManufacturerCode)
                .WithMessage("Produkt o podanym kodzie producenta już istnieje.");
        }

        private async Task<bool> BeUniqueManufacturerCode(string manufacturerCode, CancellationToken cancellationToken)
        {
            return !await _dbContext.Products
                .AnyAsync(x => x.ManufacturerCode == manufacturerCode, cancellationToken);
        }
    }
}
