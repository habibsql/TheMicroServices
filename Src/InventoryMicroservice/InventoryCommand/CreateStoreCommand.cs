namespace Inventory.Command
{
    using Common.Core;

    public class CreateStoreCommand : ICommand
    {
        public string StoreId { get; set; }

        public string ManagerName { get; set; }
    }
}
