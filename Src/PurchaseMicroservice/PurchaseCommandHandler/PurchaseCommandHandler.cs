namespace Purchase.CommandHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Common.Infrastructure;
    using Purchase.Command;
    using Purchase.Core;
    using Purchase.Domain.Model;
    using Purchase.Repository;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PurchaseCommandHandler : ICommandHandler<PurchaseCommand, CommandResult>
    {
        private readonly IPurchaseRepostiory purchaseRepository;
        private readonly IEventBus serviceBus;
        private readonly IEmailService emailService;

        public PurchaseCommandHandler(IPurchaseRepostiory purchaseRepository, IEventBus serviceBus, IEmailService emailService)
        {
            this.purchaseRepository = purchaseRepository;
            this.serviceBus = serviceBus;
            this.emailService = emailService;
        }

        public async Task<CommandResult> Handle(PurchaseCommand purchaseCommand)
        {
            CommandResult commandResponse = ValidateCommand(purchaseCommand);
            if (!commandResponse.Succeed)
            {
                return commandResponse;
            }

            Purchase purchase = Map(purchaseCommand);
            await purchaseRepository.Save(purchase);

            ProductPurchasedEvent productPurchasedEvent = Map(purchase);
            await serviceBus.Publish(Constants.MessageQueues.PurchasedQueue, productPurchasedEvent);

            EmailParams emailParams = BuildEmailParameters(productPurchasedEvent);
            await emailService.SendEmail(emailParams);

            return new CommandResult();
        }

        private CommandResult ValidateCommand(PurchaseCommand productPurchaseCommand)
        {
            var commandResponse = new CommandResult();

            if (null == productPurchaseCommand)
            {
                commandResponse.ErrorData = "Sorry! Product Purchase Command should not be null.";
            }
            else if (productPurchaseCommand.LineItems == null || productPurchaseCommand.LineItems.Count == 0)
            {
                commandResponse.ErrorData = "Sorry! Should have at least one Purchase Item.";
            }

            return commandResponse;
        }

        private Purchase Map(PurchaseCommand purchaseCommand)
        {
            var purchase = new Purchase
            {
                PurchaaseId = Guid.NewGuid().ToString(),
                PurchaseDate = DateTime.UtcNow,
                User = new User { Id = purchaseCommand.UserId, Name = purchaseCommand.UserName }
            };

            foreach (LineItemCommand lineItemCommand in purchaseCommand.LineItems)
            {
                purchase.LineItems.Add(new ProductLineItem
                {
                    Product = new Product
                    {
                        Id = lineItemCommand.ProductId,
                        ProductName = lineItemCommand.ProductName,
                        UnitPrice = lineItemCommand.UnitPrice,
                        UnitName = lineItemCommand.UnitName
                    }
                });
            }

            return purchase;
        }

        private ProductPurchasedEvent Map(Purchase purchase)
        {
            var productPurchasedEvent = new ProductPurchasedEvent
            {
                PurchaseDate = purchase.PurchaseDate
            };

            foreach (ProductLineItem productLineItem in purchase.LineItems)
            {
                var lineItem = new LineItem
                {
                    ProductId = productLineItem.Product.Id,
                    PricePerUnit = productLineItem.Unitrice,
                    Quantity = productLineItem.Quantity,
                    TotalPrice = productLineItem.TotalPrice
                };
                productPurchasedEvent.LineItems.Add(lineItem);
            }

            return productPurchasedEvent;
        }

        private EmailParams BuildEmailParameters(ProductPurchasedEvent @event)
        {
            long purchasedTotalPrice = 0;

            @event.LineItems.ToList().ForEach(item => purchasedTotalPrice += item.TotalPrice);

            var emailParams = new EmailParams
            {
                Subject = "Product Purchased",
                Body = $"The Product Purchased Amount={purchasedTotalPrice}"
            };

            emailParams.ToList.Add(Constants.EmailAddressess.AdminPurchaseEmailAddress);

            return emailParams;
        }
    }
}
