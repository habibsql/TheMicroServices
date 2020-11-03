namespace Sales.Repository
{
    using Common.Core;
    using Sales.DTO;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Sales = Domain.Sales;

    public interface ISalesRepository : IRepository<Sales>
    {
        Task<ProductSalesDTO> FindSoldProductInfo(string productId, DateTime dateFrom, DateTime dateTo);
    }
}
