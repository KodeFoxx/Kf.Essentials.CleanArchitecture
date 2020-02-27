using FluentAssertions;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.Cqs
{
    public sealed class QueryHandlerTests
    {
        public static List<Person> PeopleDatabase = new List<Person>
        {
            Person.Create(1, Name.Create("Yves", "Schelpe")),
            Person.Create(2, Name.Create("John", "Doe")),
            Person.Create(3, Name.Create("Jane", "Doe")),
        };

        [Fact]
        public async void QueryHandler_returns_result_based_on_query_model()
        {
            // Usually the queryhandler comes in populated with
            // a DbContext, FileSystem, IRepository or else
            // via DI.
            // Here we replicate it via a static list.
            var sut = new GetPeopleQueryHandler(PeopleDatabase);

            var result = await sut.HandleAsync(
                new GetPeopleQuery(amount: 1),
                CancellationToken.None
            );

            result.Should().HaveCount(1);
        }
    }
}
