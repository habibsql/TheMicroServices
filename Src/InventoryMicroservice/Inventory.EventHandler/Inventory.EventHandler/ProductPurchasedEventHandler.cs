namespace Inventory.EventHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Common.Infrastructure;
    using Inventory.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Inventory.Repository;

    public class ProductPurchasedEventHandler : IEventHandler<ProductPurchasedEvent>
    {
        private readonly IStoreItemRepository storeItemRepository;
        private readonly IStoreRepository storeRepository;

        public ProductPurchasedEventHandler(IStoreItemRepository storeItemRepository, IStoreRepository storeRepository)
        {
            this.storeItemRepository = storeItemRepository;
            this.storeRepository = storeRepository;
        }

        public async Task Handle(ProductPurchasedEvent @event)
        {
            Store store = await storeRepository.GetById("S001");
            IEnumerable<StoreItem> storeItems = Map(@event, store);

            await storeItemRepository.SaveItems(storeItems);
        }


        private IEnumerable<StoreItem> Map(ProductPurchasedEvent @event, Store store)
        {
            var storeItems = new List<StoreItem>();

            foreach (PurchasedLineItem lineItem in @event.LineItems)
            {
                var storeItem = new StoreItem
                {
                    Id = lineItem.ProductId,
                    PurchaseId = @event.PurchaseId,
                    ItemName = lineItem.ProductName,
                    Store = store,
                    Quantity = lineItem.PurchasedQuantity
                };
                storeItems.Add(storeItem);
            }

            return storeItems;
        }
    }
}
