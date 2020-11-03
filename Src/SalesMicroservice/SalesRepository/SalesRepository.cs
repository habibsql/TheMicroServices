namespace Sales.Repository
{
    using Common.Core;
    using MongoDB.Driver;
    using Sales.DTO;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sales = Domain.Sales;
    using System.Linq;
    using Sales.Domain;

    public class SalesRepository : ISalesRepository
    {
        private readonly IMongoService mongoDbService;

        public SalesRepository(IMongoService mongoDbService)
        {
            this.mongoDbService = mongoDbService;
        }

        public async Task<Sales> GetById(string id)
        {
            IMongoCollection<Sales> salesCollection = mongoDbService.GetCollection<Sales>();

            IAsyncCursor<Sales> salesCursor = await salesCollection.FindAsync(item => item.Id.Equals(id));

            return await salesCursor.FirstOrDefaultAsync();
        }

        public async Task<Sales> Save(Sales entity)
        {
            IMongoCollection<Sales> salesCollection = mongoDbService.GetCollection<Sales>();

            await salesCollection.InsertOneAsync(entity);

            return entity;
        }

        public Task<ProductSalesDTO> FindSoldProductInfo(string productId, DateTime dateFrom, DateTime dateTo)
        {
            var resultDic = new Dictionary<string, ProductSalesDTO>();
            IMongoCollection<Sales> salesCollection = mongoDbService.GetCollection<Sales>();
            IEnumerable<Sales> salesList = salesCollection.AsQueryable().Where(item => item.SalesDate >= dateFrom && item.SalesDate <= dateTo);

            long totalSalesQuantity = 0;
            string productName = string.Empty;

            foreach (Sales sales in salesList)
            {
                IEnumerable<SalesLineItem> SalesLineItems = sales.SalesLineItems.Where(item => item.ProductId.Equals(productId));

                foreach (SalesLineItem item in SalesLineItems)
                {
                    totalSalesQuantity += item.SalesQuantity;
                    if (string.IsNullOrEmpty(productName))
                    {
                        productName = item.ProductName;
                    }
                }
            }

            var proudctSalesDTO = new ProductSalesDTO
            {
                ProductId = productId,
                ProductName = productName,
                SalesQuantity = totalSalesQuantity
            };

            return Task.FromResult(proudctSalesDTO);
        }

    }
}
