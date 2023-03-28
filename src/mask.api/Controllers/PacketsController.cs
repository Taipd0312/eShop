using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using Mask.Service.Controllers;
using MediatR;

namespace mask.api.Controllers;

public class PacketsController : BaseController<Packet, Guid, string>
{
    private readonly ILogger<PacketsController> _logger;

    public PacketsController(
        IGenericRepository<Packet, Guid, string> genericRepository, 
        IMediator mediator, 
        ILogger<PacketsController> logger) : base(genericRepository, mediator)
    {
        _logger = logger;
    }
}
