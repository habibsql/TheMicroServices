namespace Purchase.Command
{
    using Common.Core;

    public class LineItemCommand : ICommand
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string PurchaseUnitName { get; set; }

        public long PurchaseUnitPrice { get; set; }

        public long PurchaseQuantity { get; set; }
    }
}
