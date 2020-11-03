namespace Common.Core
{
    using System.Threading.Tasks;

    public interface ICommandBus
    {
        Task<TResult> Send<TCommand, TResult>(TCommand command) where TCommand : ICommand
                                                                     where TResult : CommandResult;
    }
}
