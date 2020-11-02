namespace Purchase.DTO
{
    public class PurchaseItemDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int UnitPrice { get; set; }

        public string UnitName { get; set; }

        public int Quantity { get; set; }
    }
}
