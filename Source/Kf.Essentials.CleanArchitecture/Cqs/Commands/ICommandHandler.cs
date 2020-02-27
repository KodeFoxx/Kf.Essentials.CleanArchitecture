using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Cqs.Commands
{
    public interface ICommandHandler<TCommand>
        : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand
    {
        Task HandleAsync(
            TCommand command,
            CancellationToken cancellationToken);
    }

    public interface ICommandHandler<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>
    {
        Task<TCommandResult> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken);
    }
}
