using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;

namespace mask.api.Controllers;

public class PacketCheckListsController : BaseController<PacketCheckList, Guid, string>
{
    private readonly ILogger<PacketCheckListsController> _logger;

    public PacketCheckListsController(
        IGenericRepository<PacketCheckList, Guid, string> genericRepository, 
        IMediator mediator, 
        ILogger<PacketCheckListsController> logger) : base(genericRepository, mediator)
    {
        _logger = logger;
    }
}
