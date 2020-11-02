namespace Inventory.CommandHandler
{
    using Common.Core;
    using Inventory.Command;
    using Inventory.Domain;
    using Inventory.Repository;
    using System;
    using System.Threading.Tasks;

    public class CreateStoreCommandHandler : ICommandHandler<CreateStoreCommand, CommandResponse>
    {
        private readonly IStoreRepository storeRepository;

        public CreateStoreCommandHandler(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public async Task<CommandResponse> Handle(CreateStoreCommand command)
        {
            CommandResponse commandResponse = ValidateCommand(command);
            if (!commandResponse.Succeed)
            {
                return commandResponse;
            }
            Store store = Map(command);
            await storeRepository.Save(store);

            return commandResponse;
        }

        private CommandResponse ValidateCommand(CreateStoreCommand command)
        {
            var commandResponse = new CommandResponse();

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
