namespace Sales.CommandHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Sales.Command;
    using Sales.Core;
    using Sales.Domain;
    using Sales.Repository;
    using System.Threading.Tasks;
    using Sales = Sales.Domain.Sales;

    public class SalesCommandHandler : ICommandHandler<SalesCommand, CommandResult>
    {
        private readonly ISalesRepository salesRepository;
        private readonly IEventBus eventBus;

        public SalesCommandHandler(ISalesRepository salesRepository, IEventBus eventBus)
        {
            this.salesRepository = salesRepository;
            this.eventBus = eventBus;
        }

        public async Task<CommandResult> Handle(SalesCommand command)
        {
            CommandResult commandResult = await ValidateCommand(command);
            if (!commandResult.Succeed)
            {
                return commandResult;
            }

            Sales sales = Map(command);
            await salesRepository.Save(sales);

            ProductSoldEvent productSoldEvent = MapEvent(command);
            await eventBus.Publish(Constants.MessageQueue.SalesQueue, productSoldEvent);

            return commandResult;

        }

        private Task<CommandResult> ValidateCommand(SalesCommand command)
        {
            var commandResult = new CommandResult();

            if (command.SalesProducts.Count == 0)
            {
                commandResult.Error = "Sorry! Atlease 1 item should be present";
            }

            return Task.FromResult(commandResult);
        }

        private Sales Map(SalesCommand command)
        {
            var sales = new Sales
            {
                Id = command.SalesId,
                SalesDate = command.SalesDate
            };
            foreach(SalesProduct product in command.SalesProducts)
            {
                var salesLineItem = new SalesLineItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SalesQuantity = product.SalesQuantity,
                    UnitName = product.UnitName,
                    UnitSalePrice = product.SalesUnitPrice
                };
                sales.SalesLineItems.Add(salesLineItem);
            }

            return sales;
        }

        private ProductSoldEvent MapEvent(SalesCommand command)
        {
            var productSoldEvent = new ProductSoldEvent
            {
                SalesId = command.SalesId,
                SalesDate = command.SalesDate
            };
            foreach (SalesProduct product in command.SalesProducts)
            {
                var proudctSoldLineItem = new ProductSoldLineItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SoldQuantity = product.SalesQuantity,
                    SoldUnitPrice = product.SalesUnitPrice
                };
                productSoldEvent.ProductSoldLineItems.Add(proudctSoldLineItem);
            }

            return productSoldEvent;
        }
    }
}
