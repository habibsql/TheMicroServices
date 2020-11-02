namespace InventoryWebApi.Controllers
{
    using Common.Core;
    using Inventory.Command;
    using Inventory.DTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Inventory.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class StoreCommandController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;

        public StoreCommandController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Route("[action]")]
        public Task<CommandResponse> CreateStore(StoreDTO store)
        {
            var command = new CreateStoreCommand
            {
                StoreId = Guid.NewGuid().ToString(),
                ManagerName = store.ManagerName
            };

            var commandHandler = serviceProvider.GetService<ICommandHandler<CreateStoreCommand, CommandResponse>>();

           return commandHandler.Handle(command);
        }
    }
}