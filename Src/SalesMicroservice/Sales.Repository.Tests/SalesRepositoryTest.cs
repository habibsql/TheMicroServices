namespace Sales.Repository.Tests
{
    using Common.Infrastructure;
    using FluentAssertions;
    using Sales.Domain;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using Sales = Domain.Sales;

    public class SalesRepositoryTest
    {
        private readonly SalesRepository salesRepository;

        public SalesRepositoryTest()
        {
            const string connectionUrl = "mongodb://localhost:27017/SalesDB";
            var mongoDbService = new MongoService(connectionUrl);
            this.salesRepository = new SalesRepository(mongoDbService);
        }

        [Fact]
        public async Task ShouldSaveSales()
        {
            var sales = new Sales
            {
                Id = Guid.NewGuid().ToString(),
                SalesDate = DateTime.UtcNow,
            };

            sales.SalesLineItems.Add(new SalesLineItem
            {
                ProductId = "P001",
                ProductName = "Product-P001",
                UnitName = "Piece",
                UnitSalePrice = 100,
                SalesQuantity = 10
            });
            sales.SalesLineItems.Add(new SalesLineItem
            {
                ProductId = "P002",
                ProductName = "Product-P002",
                UnitName = "Piece",
                UnitSalePrice = 200,
                SalesQuantity = 2
            });

            Sales resultSales = await salesRepository.Save(sales);

            resultSales.Should().NotBeNull();
            resultSales.SalesLineItems.Should().HaveCount(2);
        }
   
        [Fact]
        public async Task ShouldReturnSalesWhenValidId()
        {
            const string salesId = "6978887e-955f-4d38-9925-379348a29a8d";

            Sales sales = await salesRepository.GetById(salesId);

            sales.Should().NotBeNull();
            sales.Id.Should().BeEquivalentTo(salesId);
        }
    }
}
