namespace Inventory.Repository
{
    using Common.Core;
    using Inventory.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStoreItemRepository : IRepository<StoreItem>
    {
        Task SaveItems(IEnumerable<StoreItem> entities);

        Task<StoreItem> UpdateItem(StoreItem entity);

        Task RemoveItem(string id);
    }
}
