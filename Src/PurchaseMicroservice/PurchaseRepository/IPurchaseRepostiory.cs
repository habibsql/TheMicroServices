namespace Purchase.Repository
{
    using Common.Core;
    using Purchase.Domain.Model;
    using Purchase.DTO;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPurchaseRepostiory : IRepository<Purchase>
    {
        public Task<IEnumerable<ProductPurchasedDTO>> SearchPurchases(DateTime from, DateTime to, int pageNumber, int pageSize, string sort, int sortDirection);
    }
}
