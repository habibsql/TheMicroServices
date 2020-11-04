namespace Inventory.EventHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Inventory.Domain;
    using Inventory.Repository;
    using System;
    using System.Threading.Tasks;

    public class ProductSoldEventHandler : IEventHandler<ProductSoldEvent>
    {
        private readonly IStoreItemRepository storeItemRepository;

        public ProductSoldEventHandler(IStoreItemRepository storeItemRepository)
        {
            this.storeItemRepository = storeItemRepository;
        }

        public async Task Handle(ProductSoldEvent @event)
        {
            foreach (ProductSoldLineItem soldItem in @event.ProductSoldLineItems)
            {
                StoreItem storeItem = await storeItemRepository.GetById(soldItem.ProductId);
                if (null == storeItem)
                    throw new ApplicationException($"Sorry! StoreItem: {soldItem.ProductId} not found in store.");

                storeItem.BalanceQuantity -= soldItem.SoldQuantity;

                await storeItemRepository.UpdateItem(storeItem);
            }
        }
    }
}
