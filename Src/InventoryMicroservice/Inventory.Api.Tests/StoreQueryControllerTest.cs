namespace Inventory.Api.Tests
{
    using Common.Core;
    using FluentAssertions;
    using Inventory.Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class StoreQueryControllerTest
    {
        private readonly HttpClient httpClient;

        public StoreQueryControllerTest()
        {
            httpClient = new TestClientProvider().HttpClient;
        }

        [Fact]
        public async Task ShouldFetchAllStoreInfo()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync("/api/storequery/stores");
            responseMessage.EnsureSuccessStatusCode();

            var storesJson = await responseMessage.Content.ReadAsStringAsync();
            var queryResult =  JsonConvert.DeserializeObject<QueryResult>(storesJson);

            queryResult.Succeed.Should().BeTrue();
            var storeList = ((JArray) queryResult.Result).ToObject<IEnumerable<Store>>();

            storeList.Should().HaveCountGreaterOrEqualTo(1);
        }
    }
}
