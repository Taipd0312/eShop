using FluentValidation;
using FluentValidation.Results;
using Mask.Application.Infrastrucetures;
using Mask.Application.Validators;
using Mask.Domain.Entities;
using Mask.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
        protected readonly IValidator<T> _validator;

        public BaseController(IGenericRepository<T, TPramaryKey, TForiegnKey> genericRepository, IMediator mediator, IValidator<T> validator)
        {
            Repo = genericRepository;
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet]
        public virtual IEnumerable<T> Get()
        {
            return Repo.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<T?> Get(TPramaryKey id)
        {
            return await Repo.GetByIdAsync(id);
        }

        [HttpPost]
        public virtual async Task<Results<MaskResult<T>, MaskValidationResponse<T>>> Post([FromBody] T value)
        {
            var validator = await _validator.ValidateAsync(value);
            await Repo.CreateAsync(value);
            await Repo.SaveChangesAsync();

            return new MaskResult<T>(value);
        }

        [HttpPut]
        public virtual async Task Put([FromBody] T value)
        {
            await Repo.UpdateAsync(value);
        }

        [HttpDelete("id")]
        public virtual async Task Delete(TPramaryKey id)
        {
            await Repo.DeleteAsync(id);
        }
    }
}
