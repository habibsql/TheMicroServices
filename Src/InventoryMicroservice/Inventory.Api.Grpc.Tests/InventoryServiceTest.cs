namespace Inventory.Api.Grpc.Tests
{
    using FluentAssertions;
    using global::Grpc.Net.Client;
    using System.Threading.Tasks;
    using Xunit;
    using static Inventory.Api.Grpc.InventoryServiceProvider;

    public class InventoryServiceTest
    {
        [Fact]
        public async Task ShouldReturnTotalItemsCountWhenValidStoreId()
        {
            using GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7001");

            var client = new InventoryServiceProviderClient(channel);

            var request = new ServiceRequest
            {
                StoreId = "S001"
            };

            ServiceReplay replay = await client.CountTotalItemsAsync(request);

            replay.ItemCount.Should().Equals("999");
        }
    }
}
