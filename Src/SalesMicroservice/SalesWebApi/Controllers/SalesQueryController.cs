namespace Sales.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sales.Query;

    [Route("api/[controller]")]
    [ApiController]
    public class SalesQueryController : ControllerBase
    {
        private readonly IQueryBus queryBus;

        public SalesQueryController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        [HttpGet("sales")]
        public Task<QueryResult> GetProudctSalesInfo(string productId, DateTime dateFrom, DateTime dateTo)
        {
            var salesQuery = new SalesQuery
            {
                ProductId = productId,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            return queryBus.Send<SalesQuery, QueryResult>(salesQuery);
        }
    }
}