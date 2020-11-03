namespace Inventory.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Core;
    using Inventory.Query;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class StoreQueryController : ControllerBase
    {
        private readonly IQueryBus queryBus;

        public StoreQueryController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        [HttpGet("stores")]
        public Task<QueryResult> GetAllStores()
        {
            return queryBus.Send<StoreQuery, QueryResult>(new StoreQuery());
        }

        [HttpGet("is-service-on")]
        public Task<bool> IsServiceOn()
        {
            return Task.FromResult(true);
        }
    }
}