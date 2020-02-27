using Kf.Essentials.CleanArchitecture.Cqs.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries
{
    public class GetPeopleQuery : IQuery<IEnumerable<Person>>
    {
        public GetPeopleQuery(int amount)
            => Amount = amount;

        public int Amount { get; }
    }

    public sealed class GetPeopleQueryHandler
        : QueryHandler<GetPeopleQuery, IEnumerable<Person>>
    {
        private readonly IEnumerable<Person> _database;

        public GetPeopleQueryHandler(IEnumerable<Person> database)
            => _database = database;

        public override async Task<IEnumerable<Person>> HandleAsync(
            GetPeopleQuery query,
            CancellationToken cancellationToken
        )
            => await Task.FromResult(
                    _database
                        .Take(query.Amount)
                        .ToList()
                        .AsEnumerable()
                );
    }
}
