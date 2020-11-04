namespace Inventory.EventHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Inventory.Domain;
    using Inventory.Repository;
    using System.Threading.Tasks;

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
            foreach (PurchasedLineItem purchasedItem in @event.LineItems)
            {
                StoreItem storeItem = await storeItemRepository.GetById(purchasedItem.ProductId);

                if (null == storeItem)
                {
                    Store store = await storeRepository.GetById("S001");

                    var newStoreItem = new StoreItem
                    {
                        Id = purchasedItem.ProductId,
                        ItemName = purchasedItem.ProductName,
                        Store = store,
                        BalanceQuantity = purchasedItem.PurchasedQuantity
                    };

                    await storeItemRepository.Save(newStoreItem);
                }
                else
                {
                    storeItem.BalanceQuantity += purchasedItem.PurchasedQuantity;

                    await storeItemRepository.UpdateItem(storeItem);
                }
            }
        }
    }
}
