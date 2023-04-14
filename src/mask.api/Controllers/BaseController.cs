using Mask.Application.Commands;
using Mask.Application.Infrastrucetures;
using Mask.Application.Interfaces;
using Mask.Application.Queries;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mask.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T, TPramaryKey, TForiegnKey> : Controller 
        where T : IEntity<TPramaryKey, TForiegnKey> 
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
        protected readonly IGenericRepository<T, TPramaryKey, TForiegnKey> Repo;
        protected readonly IMaskService<T, TPramaryKey, TForiegnKey> MaskService;
        protected readonly IMediator _mediator;

        public BaseController(IGenericRepository<T, TPramaryKey, TForiegnKey> genericRepository, IMediator mediator, IMaskService<T, TPramaryKey, TForiegnKey> maskService)
        {
            Repo = genericRepository;
            _mediator = mediator;
            MaskService = maskService;
        }

        [HttpGet]
        public async Task<MaskApplicationResponse<IEnumerable<T>>> Get()
        {
            var query = new BaseMaskCoreGetAllEntityQuery<T, TPramaryKey, TForiegnKey>();
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<MaskApplicationResponse<T>> Get(TPramaryKey id)
        {
            var query = new BaseMaskCoreGetByIdQuery<T, TPramaryKey, TForiegnKey>();
            return await _mediator.Send(query);
        }

        [HttpPost]
        public virtual async Task<MaskApplicationResponse<T>> Post([FromBody] T value)
        {
            var command = new BaseMaskCoreCreateEntityCommand<T, TPramaryKey, TForiegnKey>()
            {
                Entity = value
            };
            
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<MaskApplicationResponse<T>> Put([FromBody] T value)
        {
            var command = new BaseMaskCoreUpdateEntityCommand<T, TPramaryKey, TForiegnKey>()
            {
                Entity = value
            };

            return await _mediator.Send(command);
        }

        [HttpDelete("id")]
        public async Task<MaskApplicationResponse<TPramaryKey>> Delete(TPramaryKey id)
        {
            var command = new BaseMaskCoreDeleteEntityByIdCommand<T, TPramaryKey, TForiegnKey>()
            {
                Id = id
            };

            return await _mediator.Send(command);
        }
    }

    public class BaseMaskCoreGetByIdQuery<T, TPramaryKey, TForiegnKey> : MaskCoreGetEntityByIdQuery<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }

    public class BaseMaskCoreCreateEntityCommand<T, TPramaryKey, TForiegnKey> : MaskCoreCreateEntityCommand<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }

    public class BaseMaskCoreUpdateEntityCommand<T, TPramaryKey, TForiegnKey> : MaskCoreUpdateEntityCommand<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }

    public class BaseMaskCoreDeleteEntityCommand<T, TPramaryKey, TForiegnKey> : MaskCoreDeleteEntityCommand<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }

    public class BaseMaskCoreDeleteEntityByIdCommand<T, TPramaryKey, TForiegnKey> : MaskCoreDeleteEntityByIdCommand<T, TPramaryKey, TForiegnKey>
        where T : IEntity<TPramaryKey, TForiegnKey>
        where TPramaryKey : IComparable
        where TForiegnKey : IComparable
    {
    }
}
