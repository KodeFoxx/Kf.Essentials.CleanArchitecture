﻿namespace Kf.Essentials.CleanArchitecture.Tests.UnitTests.TestDomain
{
    public sealed class Person : DomainEntity
    {
        public static Person Empty
            => new Person();

        public static Person Create(long id, Name name)
            => new Person(id, name);
        public static Person Create(long id, string firstName, string lastName)
            => new Person(id, Name.Create(firstName, lastName));

        private Person(long id, Name name)
            : base(id)
            => Name = name;
        private Person()
            : this(0, Name.Empty)
        { }

        public Name Name { get; }

        public Person Rename(Name name)
            => new Person(Id, name);
    }
}
