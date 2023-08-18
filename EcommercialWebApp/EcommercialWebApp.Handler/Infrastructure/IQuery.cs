using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public interface IQuery<TQueryResponse> : IRequest<TQueryResponse>
    {
    }

    public interface IQuery : IRequest
    {
    }
}
