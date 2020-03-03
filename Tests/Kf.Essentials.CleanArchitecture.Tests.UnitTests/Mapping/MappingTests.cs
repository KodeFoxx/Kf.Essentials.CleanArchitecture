using AutoMapper;
using FluentAssertions;
using Kf.Essentials.CleanArchitecture.Mapping;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.UseCases;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.Mapping
{
    public sealed class MappingTests
    {
        public static List<Person> PeopleDatabase = new List<Person>
        {
            Person.Create(1, Name.Create("Yves", "Schelpe")),
            Person.Create(2, Name.Create("John", "Doe")),
            Person.Create(3, Name.Create("Jane", "Doe")),
        };

        [Fact]
        public async void CommandHandler_returns_viewmodel_from_ammping()
        {
            var mapper = new MapperConfiguration(c => c.AddProfile(new MappingProfile(GetType().Assembly))).CreateMapper();            

            // Usually the commandhandler comes in populated with
            // a DbContext, FileSystem, IRepository or else
            // via DI.
            // Here we replicate it via a static list.
            var sut = new GetPeople.Handler(
                PeopleDatabase, 
                mapper);

            var result = await sut.HandleAsync(
                new GetPeople.Command(),
                CancellationToken.None);

            result.Should().BeOfType<List<GetPeople.Result>>();
            result.Count.Should().Be(PeopleDatabase.Count);
        }
    }
}
