using FluentValidation;
using Mask.Application.Commands.ProductTypes;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mask.api.Controllers;

public class ProductTypesController : BaseController<ProductType, Guid, string>
{
    private readonly ILogger<ProductTypesController> _logger;

    public ProductTypesController(
        IGenericRepository<ProductType, Guid, string> genericRepository, 
        IMediator mediator,
        IValidator<ProductType> validator,
        ILogger<ProductTypesController> logger) : base(genericRepository, mediator, validator)
    {
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatedProductTypeAsync([FromBody] CreatedProductTypeCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}
