using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public class Broker : IBroker
    {
        private readonly IMediator _mediator;

        public Broker(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TCommandResponse> Command<TCommandResponse>(ICommand<TCommandResponse> command)
        {
            return await _mediator.Send(command);
        }

        public async Task<TCommandResponse> Command<TCommandResponse>(ICommand<TCommandResponse> command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task Command(ICommand command)
        {
            await _mediator.Send(command);
        }

        public async Task Command(ICommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task PublishEvent(INotification notification)
        {
            await _mediator.Publish(notification);
        }

        public async Task<TQueryResponse> Query<TQueryResponse>(IQuery<TQueryResponse> query)
        {
            return await _mediator.Send(query);
        }

        public async Task<TQueryResponse> Query<TQueryResponse>(IQuery<TQueryResponse> query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        public async Task Query(IQuery query)
        {
            await _mediator.Send(query);
        }

        public async Task Query(IQuery query, CancellationToken cancellationToken)
        {
            await _mediator.Send(query, cancellationToken);
        }
    }
}
