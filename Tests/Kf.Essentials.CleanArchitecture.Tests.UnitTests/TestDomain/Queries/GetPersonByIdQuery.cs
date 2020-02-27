using Kf.Essentials.CleanArchitecture.Cqs.Queries;
using LanguageExt;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries
{
    public class GetPersonByIdQuery : IQuery<Person>
    {
        public GetPersonByIdQuery(long personId)
            => PersonId = personId;

        public long PersonId { get; }
    }

    public sealed class GetPersonByIdQueryHandler
        : QueryHandler<GetPersonByIdQuery, Person>
    {
        private readonly IEnumerable<Person> _database;

        public GetPersonByIdQueryHandler(IEnumerable<Person> database)
            => _database = database;

        public override async Task<Person> HandleAsync(
            GetPersonByIdQuery query,
            CancellationToken cancellationToken
        )
            => await Task.FromResult(
                    _database
                        .FirstOrDefault(person => person.Id == query.PersonId)
                );
    }
}
