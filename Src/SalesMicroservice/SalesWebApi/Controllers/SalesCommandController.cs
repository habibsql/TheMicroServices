namespace Sales.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sales.Command;
    using Sales.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class SalesCommandController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public SalesCommandController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpPost("sales")]
        public Task<CommandResult> ProductSales([FromBody] SalesDTO salesDTO)
        {
            var salesCommand = new SalesCommand
            {
                SalesId = Guid.NewGuid().ToString(),
                SalesDate = salesDTO.SalesDate
            };
            foreach(SalesProductDTO productDTO in salesDTO.SalesProducts)
            {
                var product = new SalesProduct
                {
                    ProductId = productDTO.ProductId,
                    ProductName = productDTO.ProductName,
                    UnitName = productDTO.UnitName,
                    SalesUnitPrice = productDTO.UnitPrice,
                    SalesQuantity = productDTO.Quantity
                };
                salesCommand.SalesProducts.Add(product);
            }

            return commandBus.Send<SalesCommand, CommandResult>(salesCommand);
        }
    }
}