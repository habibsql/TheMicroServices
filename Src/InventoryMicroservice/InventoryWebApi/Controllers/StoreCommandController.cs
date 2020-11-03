namespace Inventory.Api.Controllers
{
    using Common.Core;
    using Inventory.Command;
    using Inventory.DTO;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class StoreCommandController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public StoreCommandController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpPost]
        [Route("[action]")]
        public Task<CommandResult> CreateStore(StoreDTO store)
        {
            var command = new CreateStoreCommand
            {
                StoreId = Guid.NewGuid().ToString(),
                ManagerName = store.ManagerName
            };

            return commandBus.Send<CreateStoreCommand, CommandResult>(command);
        }
    }
}