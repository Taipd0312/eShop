using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;

namespace mask.api.Controllers;

public class GoodsReceiptsController : BaseController<GoodsReceipt, Guid, string>
{
    private readonly ILogger<GoodsReceiptsController> _logger;

    public GoodsReceiptsController(
        IGenericRepository<GoodsReceipt, Guid, string> genericRepository, 
        IMediator mediator, 
        ILogger<GoodsReceiptsController> logger) : base(genericRepository, mediator)
    {
        _logger = logger;
    }
}
