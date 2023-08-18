using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public interface ICommandHandler<in TCommand, TCommandResponse> : IRequestHandler<TCommand, TCommandResponse>
        where TCommand : ICommand<TCommandResponse>
    {
    }

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
