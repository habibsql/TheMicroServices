namespace Common.Core.Events
{
    using System;
    using System.Collections.Generic;

    public class ProductPurchasedEvent : IEvent
    {
        public string PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; }

       public IList<LineItem> LineItems { get; set; }

        public ProductPurchasedEvent()
        {
            LineItems = new List<LineItem>();
        }
    }

    public class LineItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public int PricePerUnit { get; set; }

        public int TotalPrice { get; set; }
    }
}
