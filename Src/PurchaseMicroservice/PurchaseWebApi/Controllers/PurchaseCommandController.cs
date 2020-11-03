namespace Purchase.Api.Controllers
{
    using Common.Core;
    using Microsoft.AspNetCore.Mvc;
    using Purchase.Command;
    using Purchase.DTO;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseCommandController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public PurchaseCommandController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpPost("purchase")]
        public Task<CommandResult> Purchase([FromBody] PurchaseDTO purchaseDTO)
        {
            var command = new PurchaseCommand
            {
                PurchaseId = Guid.NewGuid().ToString(),
                PurchaseDate = purchaseDTO.PurchaseDate
            };
            command.LineItems = new List<LineItemCommand>();

            foreach (var lineItemDTO in purchaseDTO.PurchaseItems ?? new List<PurchaseItemDTO>())
            {
                command.LineItems.Add(new LineItemCommand
                {
                    ProductId = lineItemDTO.ProductId,
                    ProductName = lineItemDTO.ProductName,
                    PurchaseQuantity = lineItemDTO.Quantity,
                    UnitName = lineItemDTO.UnitName
                });
            }

            return commandBus.Send<PurchaseCommand, CommandResult>(command);
        }

        [HttpPost("ping")]
        [HttpGet("ping")]
        public string Ping()
        {
            return new Random().Next().ToString();
        }
    }
}
