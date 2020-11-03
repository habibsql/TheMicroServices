namespace Purchase.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Purchase.DTO;
    using Purchase.Query;

    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseQueryController : ControllerBase
    {
        private readonly IQueryBus queryBus;

        public PurchaseQueryController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        [HttpGet("purchase")]
        public Task<QueryResult> GetPurchasedData([FromQuery] PurchaseQueryDTO query)
        {
            var productPurchaseQuery = new ProductPurchasedQuery
            {
                DateFrom = query.DateFrom,
                DateTo = query.DateTo,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortFiled = query.SortField,
                SortDirection = query.SortDirection
            };

            return queryBus.Send<ProductPurchasedQuery, QueryResult>(productPurchaseQuery);
        }
    }
}