using MediatR;

namespace EcommercialWebApp.Handler.Infrastructure
{
    public interface IQueryHandler<in TQuery, TQueryResponse> : IRequestHandler<TQuery, TQueryResponse>
        where TQuery : IQuery<TQueryResponse>
    {
    }

    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
        where TQuery : IQuery
    {
    }
}
