namespace Purchase.DTO
{
    public class PurchaseItemDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public long UnitPrice { get; set; }

        public string UnitName { get; set; }

        public long Quantity { get; set; }
    }
}
