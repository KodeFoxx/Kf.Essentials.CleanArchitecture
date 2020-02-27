using LanguageExt;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Cqs.Queries
{
    public abstract class QueryHandler<TQuery, TQueryResult>
        : IQueryHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
        public abstract Task<TQueryResult> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken);
    }
}
