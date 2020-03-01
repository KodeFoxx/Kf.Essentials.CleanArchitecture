using AutoMapper;

namespace Kf.Essentials.CleanArchitecture.Cqs
{
    public abstract class Dto : IDto
    { }

    public abstract class Dto<TDomain> : Dto, IDto<TDomain>
    {
        public virtual void Mapping(Profile profile)
            => profile.CreateMap(typeof(TDomain), GetType());
    }
}