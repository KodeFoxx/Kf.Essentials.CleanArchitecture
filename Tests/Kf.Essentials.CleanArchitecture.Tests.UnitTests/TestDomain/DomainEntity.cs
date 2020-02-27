using Kf.Essentials.CleanArchitecture.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain
{
    public abstract class DomainEntity : Entity<long>
    {
        protected DomainEntity(long id)
            : base(id)
        { }
    }
}
