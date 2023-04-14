using MediatR;

namespace Mask.Application.CQRSs
{
    public interface ICommand : IRequest
    { 
    }

    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    { 
    }

    public interface ICommandHandler<in T, TResponse> : IRequestHandler<T, TResponse> where T : ICommand<TResponse>
    {
    }

    public interface ICommandBus
    {
        Task Send(ICommand command);

        Task<TResponse> Send<TResponse>(ICommand<TResponse> command);
    }

    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;

        public CommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Send(ICommand command)
        {
            await mediator.Send(command);
        }

        public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command)
        {
            return await mediator.Send(command);
        }
    }
}
