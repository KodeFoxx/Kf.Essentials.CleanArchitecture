using Kf.Essentials.CleanArchitecture.Cqs.Queries;
using System.Collections.Generic;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain.Queries
{
    public class GetPeopleQuery : IQuery<IEnumerable<Person>>
    {
        public GetPeopleQuery(int amount)
            => Amount = amount;

        public int Amount { get; }
    }
}
