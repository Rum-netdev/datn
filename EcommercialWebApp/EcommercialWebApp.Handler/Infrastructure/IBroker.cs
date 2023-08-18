using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public interface IBroker
    {
        Task<TCommandResponse> Command<TCommandResponse>(ICommand<TCommandResponse> command);
        Task<TCommandResponse> Command<TCommandResponse>(ICommand<TCommandResponse> command, CancellationToken cancellationToken);
        Task Command(ICommand command);
        Task Command(ICommand command, CancellationToken cancellationToken);
        Task<TQueryResponse> Query<TQueryResponse>(IQuery<TQueryResponse> query);
        Task<TQueryResponse> Query<TQueryResponse>(IQuery<TQueryResponse> query, CancellationToken cancellationToken);
        Task Query(IQuery query);
        Task Query(IQuery query, CancellationToken cancellationToken);
        Task PublishEvent(INotification notification);
    }
}
