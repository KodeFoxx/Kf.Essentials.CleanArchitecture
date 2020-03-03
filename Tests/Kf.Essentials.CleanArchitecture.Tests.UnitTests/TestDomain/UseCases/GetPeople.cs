using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kf.Essentials.CleanArchitecture.Cqs;
using Kf.Essentials.CleanArchitecture.Cqs.Commands;
using Kf.Essentials.CleanArchitecture.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.UseCases
{
    public sealed class GetPeople
    {
        public sealed class Command : Command<List<Result>>
        {            
        }

        public class Handler : CommandHandler<Command, List<Result>>
        {
            private readonly IEnumerable<Person> _database;
            private readonly IMapper _mapper;

            public Handler(
                IEnumerable<Person> database,
                IMapper mapper
            )
            {
                _database = database;
                _mapper = mapper;
            }

            public async override Task<List<Result>> HandleAsync(
                Command command,
                CancellationToken cancellationToken
            )
                => await Task.Run(() => _database
                    .AsQueryable()
                    .ProjectTo<Result>(_mapper.ConfigurationProvider)
                    .ToList());
        }

        public sealed class Result : IMapFrom<Person>
        {
            public long Id { get; }
            public string FirstName { get; }
            public string LastName { get; }            

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Person, Result>()
                    .ForMember(vm => vm.FirstName, m => m.MapFrom(p => p.Name.First))
                    .ForMember(vm => vm.LastName, m => m.MapFrom(p => p.Name.Last));
            }
        }
    }    
}
