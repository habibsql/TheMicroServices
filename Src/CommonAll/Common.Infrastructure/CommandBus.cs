namespace Common.Infrastructure
{
    using Common.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Delegate commands with its handler with the help of IOC 
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> Send<TCommand, TResult>(TCommand command) 
            where TCommand : ICommand 
            where TResult : CommandResult
        {
            var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();

            return await commandHandler.Handle(command);
        }
    }
}
