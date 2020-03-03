using Kf.Essentials.CleanArchitecture.Cqs.Commands;
using Kf.Essentials.CleanArchitecture.Cqs.Queries;
using Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries;
using LanguageExt;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.UseCases
{
    public sealed class UpdateName
    {
        public class Command : Command<Name>
        {
            public Command(Person person, Name newName)
            {
                Person = person;
                NewName = newName;
            }

            public Person Person { get; }
            public Name NewName { get; }
        }

        public class Handler : CommandHandler<Command, Name>
        {
            private readonly IEnumerable<Person> _database;
            private readonly IQueryHandler<GetPersonByIdQuery, Person> _getPersonByIdHandler;

            public Handler(
                IEnumerable<Person> database,
                IQueryHandler<GetPersonByIdQuery, Person> getPersonByIdHandler)
            {
                _database = database;
                _getPersonByIdHandler = getPersonByIdHandler;
            }

            public override async Task<Name> HandleAsync(
                Command command, 
                CancellationToken cancellationToken)
            {
                var person = await _getPersonByIdHandler.HandleAsync(
                        new GetPersonByIdQuery(command.Person.Id),
                        cancellationToken
                    );

                person = person.Rename(command.NewName);

                // mimics the save a save method on a dbcontext, json file, or whatever
                ((List<Person>)_database).Remove(person);
                ((List<Person>)_database).Add(person);

                return person.Name;                    
            }
        }
    }
}
