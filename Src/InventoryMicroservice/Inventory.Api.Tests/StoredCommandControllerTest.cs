namespace Inventory.Api.Tests
{
    using FluentAssertions;
    using Inventory.DTO;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class StoredCommandControllerTest
    {
        private readonly HttpClient httpClient;

        public StoredCommandControllerTest()
        {
            var testServerProvider = new TestClientProvider();
            this.httpClient = testServerProvider.HttpClient;
        }

        [Fact]
        public async Task ShouldCreateStore()
        {
            var storeDTO = new StoreDTO
            {
                StoreId = "S001",
                ManagerName = "Mr. Manager"
            };

            var storeJson = JsonConvert.SerializeObject(storeDTO);
            var content = new StringContent(storeJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/StoreCommand/CreateStore", content);

            response.EnsureSuccessStatusCode();

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
