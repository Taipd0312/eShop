using Mask.Application.Commands.ProductTypes;
using Mask.Application.Interfaces;
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
        IMaskService<ProductType, Guid, string> maskService, 
        IMediator mediator, 
        ILogger<ProductTypesController> logger) : base(genericRepository, mediator, maskService)
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
