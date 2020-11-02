namespace PurchaseRepository
{
    using Common.Core;
    using Purchase.Core;
    using Purchase.Domain.Model;
    using Purchase.Repository;
    using System;
    using System.Threading.Tasks;

    public class PurchaseRepository : IPurchaseRepostiory
    {
        private readonly IMongoDbService mongoDbService;

        public PurchaseRepository(IMongoDbService mongoDbService)
        {
            this.mongoDbService = mongoDbService;
        }

        public Task<Purchase> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> Save(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
