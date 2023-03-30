using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;

namespace mask.api.Controllers;

public class ProductColorsController : BaseController<ProductColor, Guid, string>
{
    private readonly ILogger<ProductColorsController> _logger;

    public ProductColorsController(
        IGenericRepository<ProductColor, Guid, string> genericRepository, 
        IMediator mediator, 
        ILogger<ProductColorsController> logger) : base(genericRepository, mediator)
    {
        _logger = logger;
    }
}
