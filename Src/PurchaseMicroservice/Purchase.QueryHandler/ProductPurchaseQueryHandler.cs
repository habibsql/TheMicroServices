namespace Purchase.QueryHandler
{
    using Common.Core;
    using Purchase.Query;
    using Purchase.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductPurchaseQueryHandler : IQueryHandler<ProductPurchasedQuery, QueryResult>
    {
        private readonly IPurchaseRepostiory purchaseRepository;

        public ProductPurchaseQueryHandler(IPurchaseRepostiory purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        public async Task<QueryResult> Handle(ProductPurchasedQuery query)
        {
            QueryResult queryResult = await ValidateQuery(query);

            if (!queryResult.Succeed)
            {
                return queryResult;
            }

            queryResult.Result = await purchaseRepository.SearchPurchases(query.DateFrom, query.DateTo, query.PageNumber, query.PageSize, query.SortFiled, query.SortDirection);


            return queryResult;
        }

        private Task<QueryResult> ValidateQuery(ProductPurchasedQuery query)
        {
            var queryResult = new QueryResult();

            if (query.DateFrom > query.DateTo)
            {
                queryResult.Error = $"Sorry! DateFrom should not be greter than DateTo";
            }

            return Task.FromResult(queryResult);
        }
    }
}
