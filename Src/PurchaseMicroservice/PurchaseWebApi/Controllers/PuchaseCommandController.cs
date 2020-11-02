namespace Purchase.WebApi.Controllers
{
    using Common.Core;
    using Microsoft.AspNetCore.Mvc;
    using Purchase.Command;
    using Purchase.DTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    [ApiController]
    public class PuchaseCommandController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public PuchaseCommandController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpPost]
        [Route("purchases/do")]
        public Task<CommandResponse> DoPurchase(PurchaseDTO purchaseDTO)
        {
            var command = new PurchaseCommand
            {                
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

            return commandBus.Route<PurchaseCommand, CommandResponse>(command);
        }
    }
}
