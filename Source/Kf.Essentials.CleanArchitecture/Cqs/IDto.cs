using Kf.Essentials.CleanArchitecture.Mapping;

namespace Kf.Essentials.CleanArchitecture.Cqs
{
    public interface IDto 
    { }

    public interface IDto<TDomain> : IDto, IMapFrom<TDomain>
    { }
}
