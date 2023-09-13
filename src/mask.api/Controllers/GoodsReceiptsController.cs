using FluentValidation;
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
        IValidator<GoodsReceipt> validator,
        ILogger<GoodsReceiptsController> logger) : base(genericRepository, mediator, validator)
    {
        _logger = logger;
    }
}
