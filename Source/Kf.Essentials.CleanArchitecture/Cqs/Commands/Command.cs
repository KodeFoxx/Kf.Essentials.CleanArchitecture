namespace Kf.Essentials.CleanArchitecture.Cqs.Commands
{
    public abstract class Command : ICommand
    {
    }

    public abstract class Command<TResult> : ICommand<TResult>
    {
    }
}
