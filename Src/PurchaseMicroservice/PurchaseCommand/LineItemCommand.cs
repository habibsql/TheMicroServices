namespace Purchase.Command
{
    using Common.Core;

    public class LineItemCommand : ICommand
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string UnitName { get; set; }

        public int UnitPrice { get; set; }

        public int PurchaseQuantity { get; set; }
    }
}
