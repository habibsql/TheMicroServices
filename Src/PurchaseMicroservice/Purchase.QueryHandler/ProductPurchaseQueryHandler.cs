namespace Purchase.QueryHandler
{
    using Common.Core;
    using Purchase.Query;
    using Purchase.Repository;
    using System.Threading.Tasks;

    public class ProductPurchaseQueryHandler : IQueryHandler<ProductPurchasedQuery, QueryResult>
    {
        private readonly IPurchaseRepostiory purchaseRepository;

        public ProductPurchaseQueryHandler(IPurchaseRepostiory purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        public Task<QueryResult> Handle(ProductPurchasedQuery query)
        {


            return null;
        }
    }
}
