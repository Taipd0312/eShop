using MediatR;

namespace Mask.Application.CQRSs
{
    public interface ICommand : IRequest
    { 
    }

    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    { 
    }

    public interface ICommandBus
    {
        Task Send(ICommand command);
    }

    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;

        public CommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Send(ICommand command)
        {
            return mediator.Send(command);
        }
    }
}
