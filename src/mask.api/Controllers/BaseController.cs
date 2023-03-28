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
        protected readonly IMediator _mediator;

        public BaseController(IGenericRepository<T, TPramaryKey, TForiegnKey> genericRepository, IMediator mediator)
        {
            Repo = genericRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<T> Get()
        {
            return Repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<T?> Get(TPramaryKey id)
        {
            return await Repo.GetByIdAsync(id);
        }

        [HttpPost]
        public virtual async Task Post([FromBody] T value)
        {
            await Repo.CreateAsync(value);
        }

        [HttpPut]
        public async Task Put([FromBody] T value)
        {
            await Repo.UpdateAsync(value);
        }

        [HttpDelete("id")]
        public async Task Delete(TPramaryKey id)
        {
            await Repo.DeleteAsync(id);
        }
    }
}
