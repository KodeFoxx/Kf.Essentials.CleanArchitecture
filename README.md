![CI @ ubuntu-latest](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/workflows/CI%20@%20ubuntu-latest/badge.svg)
![GitHub top language](https://img.shields.io/github/languages/top/kodefoxx/kf.essentials.cleanarchitecture)

# Essentials for Clean Architecture
`Kf.Essentials.CleanArchitecture` are helper classes, structures and extension methods used to form a base for pragmatic, clean architecture minded design.
> You can [download/get it on NuGet](https://www.nuget.org/packages/Kf.Essentials.CleanArchitecture/)

The classes/tools available are:
- [Domain modelling](#domainmodelling)
  - [`Entity<TId>`](#entityoftid)
  - [`ValueObject`](#valueobject)

## Dependencies
- [language-ext](https://github.com/louthy/language-ext)
- [MediatR](https://github.com/jbogard/MediatR)
- [KF.Essentials](https://github.com/KodeFoxx/Kf.Essentials)

## <a name="domainmodelling" /> Domain modelling
We draw a lot from the original _Domain Driven Design_ guidelines here, and implement it in a pragmatic way. If you do not agree with this setup, there is flexibility to alter it when implementing, or else, simply ignore it.
Do feel free to post an issue if something might be broken. 

If you want some reading on what an entity and a value object represents, please read this blgpost [Entity vs Value Object: the ultimate list of differences](https://enterprisecraftsmanship.com/posts/entity-vs-value-object-the-ultimate-list-of-differences/).

### <a name="entityoftid" /> Entity<TId> 
<small style="font-size: 10px">([see implementation](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/blob/master/Source/Kf.Essentials.CleanArchitecture/Entity.cs))</small>
Abstract generic class giving you the benefit of not writing boilerplate for an entity base class. `TId` stands for the type of the id you use in your domain model. It is recommended when started your own project not just to implement directly from `Entity<TId>`, but to create your own implementation based on it. E.g.:
```
    public abstract class DomainEntity : Entity<long>
    {
        protected DomainEntity(long id) 
            : base(id)
        { }
    }

    public sealed class Person : DomainEntity
    {
        // rest of the implementation here...
    }
```

Furthermore if you use a non-native type for your id, please consider overriding the [`CompareId` method](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/blob/master/Source/Kf.Essentials.CleanArchitecture/Entity.cs#L53) as this is implemented in the [`Equals` method](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/blob/master/Source/Kf.Essentials.CleanArchitecture/Entity.cs#L46). Overriding it means you can apply specific logic for your id type, or apply other domain specific logic determining the equality between two id's.
```
    public abstract class DomainEntity : Entity<long>
    {
        protected DomainEntity(long id) 
            : base(id)
        { }

        protected override bool CompareId(long a, long b)
        {
            // implement your custom compare logic between a and b  
	}
    }    
```

Last of all, I refer to the [TestDomain](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/tree/master/Tests/Kf.Essentials.CleanArchitecture.Tests.UnitTests/TestDomain) for example usage.

### <a name="valueobject" /> ValueObject 
<small style="font-size: 10px">([see implementation](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/blob/master/Source/Kf.Essentials.CleanArchitecture/ValueObject.cs))</small>
Abstract class that provides you the benefit of not writing boilerplate for a valueobject base class.
When inheriting from in you will need to override the [property `EquatableValues`](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/blob/master/Source/Kf.Essentials.CleanArchitecture/ValueObject.cs#L35), this is an `IEnumerable<object>` that represents all properties, fields in the object that need to be taken into account when being compared, it essentially defines how `Equals` and `HashCode` are calculated and thus defines the _"identity"_ of the object. Here's an example implementation.
```
    public sealed class Name : ValueObject
    {
        // rest of the implementation here...

        public string First { get; }
        public string Last { get; }        

        protected override IEnumerable<object> EquatableValues
            => new[] { First, Last };
    }
```

Last of all, I refer to the [TestDomain](https://github.com/KodeFoxx/Kf.Essentials.CleanArchitecture/tree/master/Tests/Kf.Essentials.CleanArchitecture.Tests.UnitTests/TestDomain) for example usage.
