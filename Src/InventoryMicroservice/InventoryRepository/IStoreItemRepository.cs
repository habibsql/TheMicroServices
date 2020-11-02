namespace Inventory.Repository
{
    using Common.Core;
    using Inventory.Domain;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStoreItemRepository : IRepository<StoreItem>
    {
        public Task SaveItems(IEnumerable<StoreItem> entities);
    }
}
