using Crud.DDD.Core.Aggregates.ProductAggregate.Services;
using Crud.DDD.Core.Aggregates.ProductAggregate.ValueObjects;
using FluentValidation;
using MediatR;

namespace Crud.DDD.Application.Features.Product.Commands
{
    public record CreateProductCommand(string Name, string SKU, Guid CatalogId) : IRequest;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(p => p.CatalogId)
              .NotNull()
              .NotEmpty()
              .WithMessage("CatalogId is required");

            RuleFor(p => p.SKU)
               .NotNull()
               .NotEmpty()
               .MaximumLength(SKU.SKU_MAX_LENGTH)
               .MinimumLength(SKU.SKU_MIN_LENGTH)
               .WithMessage($"SKU is required and SKU length should be between {SKU.SKU_MIN_LENGTH} and {SKU.SKU_MAX_LENGTH}");
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductManager _productManager;

        public CreateProductCommandHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = DDD.Core.Aggregates.ProductAggregate
                .Product.Create(request.CatalogId, request.Name, request.SKU);

            await _productManager.AddAsync(product, cancellationToken);
        }
    }

}
