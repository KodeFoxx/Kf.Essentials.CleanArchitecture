using MediatR;

namespace Kf.Essentials.CleanArchitecture.Cqs.Commands
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }    
}
