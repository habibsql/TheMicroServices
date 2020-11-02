namespace Inventory.CommandHandler
{
    using Common.Core;
    using Inventory.Command;
    using Inventory.Domain;
    using Inventory.Repository;
    using System;
    using System.Threading.Tasks;

    public class CreateStoreCommandHandler : ICommandHandler<CreateStoreCommand, CommandResult>
    {
        private readonly IStoreRepository storeRepository;

        public CreateStoreCommandHandler(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public async Task<CommandResult> Handle(CreateStoreCommand command)
        {
            CommandResult commandResponse = ValidateCommand(command);
            if (!commandResponse.Succeed)
            {
                return commandResponse;
            }
            Store store = Map(command);
            await storeRepository.Save(store);

            return commandResponse;
        }

        private CommandResult ValidateCommand(CreateStoreCommand command)
        {
            var commandResponse = new CommandResult();

            if (string.IsNullOrEmpty(command.ManagerName))
            {
                commandResponse.ErrorData = "Sorry! Manager name should not be empty.";
            }

            return commandResponse;
        }

        private Store Map(CreateStoreCommand command)
        {
            return new Store
            {
                Id = command.StoreId,
                Manager = command.ManagerName
            };
        }
    }
}
