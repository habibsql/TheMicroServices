namespace Common.Core
{
    using System.Threading.Tasks;

    public interface ICommandBus
    {
        Task<TResponse> Route<TCommand, TResponse>(TCommand command);
    }
}
