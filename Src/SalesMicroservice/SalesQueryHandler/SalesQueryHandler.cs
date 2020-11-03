namespace Sales.QueryHandler
{
    using Common.Core;
    using Sales.DTO;
    using Sales.Query;
    using Sales.Repository;
    using System.Threading.Tasks;

    public class SalesQueryHandler : IQueryHandler<SalesQuery, QueryResult>
    {
        private readonly ISalesRepository salesRepository;

        public SalesQueryHandler(ISalesRepository salesRepository)
        {
            this.salesRepository = salesRepository;
        }

        public async Task<QueryResult> Handle(SalesQuery query)
        {
            ProductSalesDTO productSalesDTO = await salesRepository.FindSoldProductInfo(query.ProductId, query.DateFrom, query.DateTo);

            return new QueryResult { Result = productSalesDTO };
        }
    }
}
