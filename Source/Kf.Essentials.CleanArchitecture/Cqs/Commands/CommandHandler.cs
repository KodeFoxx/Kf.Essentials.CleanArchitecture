using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kf.Essentials.CleanArchitecture.Cqs.Commands
{
    public abstract class CommandHandler<TCommand>
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
        public async Task<Unit> Handle(
            TCommand request, 
            CancellationToken cancellationToken)
        {
            await HandleAsync(request, cancellationToken);
            return Unit.Value;
        }
    }

    public abstract class CommandHandler<TCommand, TCommandResult>
        : ICommandHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>
    {
        public abstract Task<TCommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
        public async Task<TCommandResult> Handle(
            TCommand request,
            CancellationToken cancellationToken
        )
            => await HandleAsync(request, cancellationToken);
    }
}
