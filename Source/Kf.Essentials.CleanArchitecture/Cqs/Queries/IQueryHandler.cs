using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Cqs.Queries
{
    public interface IQueryHandler<in TQuery, TQueryResult>
        : IRequestHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken);
    }
}
