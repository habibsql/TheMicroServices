namespace Inventory.EventHandler
{
    using Common.Core;
    using Common.Core.Events;
    using Inventory.Repository;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;

    public class ProductSoldEventHandler : IEventHandler<ProductSoldEvent>
    {
        private readonly IStoreItemRepository storeItemRepository;

        public ProductSoldEventHandler( IStoreItemRepository storeItemRepository)
        {
            this.storeItemRepository = storeItemRepository;
        }

        public async Task Handle(ProductSoldEvent @event)
        {
            IEnumerable<string> productIds = @event.ProductSoldLineItems.Select(item => item.ProductId);

            await storeItemRepository.RemoveItems(productIds);
        }
    }
}
