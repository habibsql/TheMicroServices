
namespace Inventory.Repository
{
    using Common.Core;
    using Inventory.Domain;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StoreItemRepository : IStoreItemRepository
    {
        private readonly IMongoService mongoService;

        public StoreItemRepository(IMongoService mongoDbService)
        {
            this.mongoService = mongoDbService;
        }

        public async Task<StoreItem> GetById(string id)
        {
            IMongoCollection<StoreItem> storeItemCollection = mongoService.GetCollection<StoreItem>();

            IAsyncCursor<StoreItem> itemCursor = await storeItemCollection.FindAsync(item => item.Id.Equals(id));

            return await itemCursor.FirstOrDefaultAsync();
        }

        public async Task RemoveItem(string id)
        {
            IMongoCollection<StoreItem> storeItemCollection = mongoService.GetCollection<StoreItem>();

            await storeItemCollection.DeleteOneAsync(item => item.Id.Equals(id));
        }

        public async Task<StoreItem> Save(StoreItem entity)
        {
            IMongoCollection<StoreItem> storeItemCollection = mongoService.GetCollection<StoreItem>();

            await storeItemCollection.InsertOneAsync(entity);

            return entity;
        }

        public async Task SaveItems(IEnumerable<StoreItem> entities)
        {
            foreach (StoreItem item in entities)
            {
                await Save(item);
            }
        }

        public async Task<StoreItem> UpdateItem(StoreItem entity)
        {
            IMongoCollection<StoreItem> storeItemCollection = mongoService.GetCollection<StoreItem>();
            FilterDefinition<StoreItem> criterial = Builders<StoreItem>.Filter.Eq(item => item.Id, entity.Id);

            await storeItemCollection.ReplaceOneAsync(criterial, entity);

            return entity;
        }
    }
}
