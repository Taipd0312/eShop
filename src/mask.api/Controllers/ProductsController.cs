using FluentValidation;
using Mask.Application.Queries.Products;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mask.api.Controllers;

public class ProductsController : BaseController<Product, Guid, string>
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        IGenericRepository<Product, Guid, string> genericRepository, 
        IMediator mediator, 
        IValidator<Product> validator,
        ILogger<ProductsController> logger) : base(genericRepository, mediator, validator)
    {
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<IEnumerable<Product>> SearchProductsAsync([FromQuery] GetAllProductsQuery query)
    {
        return await _mediator.Send(query);
    }
}
