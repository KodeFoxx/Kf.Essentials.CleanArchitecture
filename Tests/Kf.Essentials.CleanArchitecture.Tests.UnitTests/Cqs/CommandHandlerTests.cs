using FluentAssertions;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.UseCases;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.Cqs
{
    public sealed class CommandHandlerTests
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
            var newName = Name.Create("New", "Name");

            // Usually the commandhandler comes in populated with
            // a DbContext, FileSystem, IRepository or else
            // via DI.
            // Here we replicate it via a static list.
            var sut = new UpdateName.Handler(
                PeopleDatabase, 
                new GetPersonByIdQueryHandler(PeopleDatabase));            

            var result = await sut.HandleAsync(
                new UpdateName.Command(PeopleDatabase.First(), newName),
                CancellationToken.None
            );

            result.Should().Be(newName);
            PeopleDatabase.FirstOrDefault(p => p.Id == 1).Name.Should().Be(newName);
        }        
    }
}
