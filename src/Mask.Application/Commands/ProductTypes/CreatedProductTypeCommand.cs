using FluentValidation;
using Mask.Application.CQRSs;
using Mask.Application.Interfaces.ProductTypes;
using Mask.Domain.Entities;
using Mask.Domain.Enums;
using MediatR;

namespace Mask.Application.Commands.ProductTypes
{
    public class CreatedProductTypeCommand : ICommand
    {
        public string? Name { get; set; }

        public ProductUnitEnums Unit { get; set; }
    }

    public class CreatedProductTypeCommandHandler : ICommandHandler<CreatedProductTypeCommand>
    {
        private readonly IProductTypeService productTypeService;

        public CreatedProductTypeCommandHandler(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        public async Task<Unit> Handle(CreatedProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = new ProductType
            {
                Id = Guid.NewGuid(),
                Name = request.Name!,
                Unit = request.Unit,
                Version = 1,
                CreatedBy = "CurrentUser",
                CreatedOn = DateTime.UtcNow,
                ModifiedBy = "CurrentUser",
                ModifiedOn = DateTime.UtcNow
            };

            var newProductType = await productTypeService.CreateProductTypeAsync(productType);

            return Unit.Value;
        }
    }

    public class CreateProductCommndValidator : AbstractValidator<CreatedProductTypeCommand>
    {
        public CreateProductCommndValidator()
        {
            RuleFor(c => c.Unit).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
