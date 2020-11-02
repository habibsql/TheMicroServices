
namespace Inventory.Repository
{
    using Common.Core;
    using Inventory.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StoreItemRepository : IStoreItemRepository
    {
        private readonly IMongoDbService mongoDbService;

        public StoreItemRepository(IMongoDbService mongoDbService)
        {
            this.mongoDbService = mongoDbService;
        }

        public Task<StoreItem> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<StoreItem> Save(StoreItem entity)
        {
            return Task.FromResult(entity);
        }

        public Task SaveItems(IEnumerable<StoreItem> entities)
        {
            throw new NotImplementedException();
        }

    }
}
