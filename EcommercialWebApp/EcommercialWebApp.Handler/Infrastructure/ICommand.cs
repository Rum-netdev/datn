using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public interface ICommand<TCommandResponse> : IRequest<TCommandResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
