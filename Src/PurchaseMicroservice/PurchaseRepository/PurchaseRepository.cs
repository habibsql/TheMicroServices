namespace Purchase.Repository
{
    using Common.Core;
    using MongoDB.Driver;
    using Purchase.Domain.Model;
    using Purchase.DTO;
    using Purchase.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PurchaseRepository : IPurchaseRepostiory
    {
        private readonly IMongoService mongoDbService;

        public PurchaseRepository(IMongoService mongoDbService)
        {
            this.mongoDbService = mongoDbService;
        }

        public async Task<Purchase> GetById(string id)
        {
            IMongoCollection<Purchase> purchaseCollection = mongoDbService.GetCollection<Purchase>();

            IAsyncCursor<Purchase> purchaseCursor = await purchaseCollection.FindAsync(item => item.Id.Equals(id));

            return await purchaseCursor.FirstOrDefaultAsync();
        }

        public async Task<Purchase> Save(Purchase purchase)
        {
            IMongoCollection<Purchase> purchaseCollection = mongoDbService.GetCollection<Purchase>();

            await purchaseCollection.InsertOneAsync(purchase);

            return purchase;
        }

        public Task<IEnumerable<ProductPurchasedDTO>> SearchPurchases(DateTime from, DateTime to, int pageNumber, int pageSize, string sortField, int sortDirection)
        {
            IMongoCollection<Purchase> purchaseCollection = mongoDbService.GetCollection<Purchase>();

            int skip = (pageNumber - 1) * pageSize;

            IQueryable<Purchase> query = purchaseCollection.AsQueryable().Where(item => item.PurchaseDate >= from && item.PurchaseDate <= to);

            var orderBy = sortDirection == 0 ? query.OrderBy(item => sortField) : query.OrderByDescending(item => sortField);
           
            IEnumerable <Purchase> purchases = query.Skip(skip).Take(pageSize);

            IEnumerable<ProductPurchasedDTO> productPurchasedDtoList = Map(purchases);

            return Task.FromResult(productPurchasedDtoList);
        }

        private IEnumerable<ProductPurchasedDTO> Map(IEnumerable<Purchase> purchases)
        {
            var dtoList = new List<ProductPurchasedDTO>();

            foreach (Purchase purchase in purchases)
            {
                foreach (ProductLineItem productLineItem in purchase.LineItems)
                {
                    var productPurchasedDTO = new ProductPurchasedDTO
                    {
                        Date = purchase.PurchaseDate,
                        ProductName = productLineItem.Product.ProductName,
                        PurchasedQuantity = productLineItem.Quantity,
                        PurchasedAmount = productLineItem.TotalPrice
                    };

                    dtoList.Add(productPurchasedDTO);
                }
            }

            return dtoList;
        }
    }
}
