namespace Inventory.Api.Grpc
{
    using global::Grpc.Core;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class InventoryService : InventoryServiceProvider.InventoryServiceProviderBase
    {

        public InventoryService()
        {
        }

        public override Task<ServiceReplay> CountTotalItems(ServiceRequest request, ServerCallContext context)
        {
            // Need to fetch from db. Now it is hardcode value

            return Task.FromResult(new ServiceReplay { ItemCount = "999" });
        }
    }
}
