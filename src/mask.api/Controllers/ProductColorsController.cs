using Mask.Application.Interfaces;
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
        IMaskService<ProductColor, Guid, string> maskService, 
        IMediator mediator, 
        ILogger<ProductColorsController> logger) : base(genericRepository, mediator, maskService)
    {
        _logger = logger;
    }
}
