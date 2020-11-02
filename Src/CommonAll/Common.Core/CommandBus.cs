namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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

        public async Task<TResponse> Route<TCommand, TResponse>(TCommand command)
        {
            var commandHandler = (ICommandHandler<TCommand, TResponse>)serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>));

            return await commandHandler.Handle(command);
        }
    }
}
