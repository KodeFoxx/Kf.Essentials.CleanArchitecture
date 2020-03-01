using MediatR;

namespace Kf.Essentials.CleanArchitecture.Cqs.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
