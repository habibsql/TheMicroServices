namespace Common.Core.Events
{
    using System;
    using System.Collections.Generic;

    public class ProductPurchasedEvent : IEvent
    {
        public string PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; }

       public IList<PurchasedLineItem> LineItems { get; set; }

        public ProductPurchasedEvent()
        {
            LineItems = new List<PurchasedLineItem>();
        }
    }

    public class PurchasedLineItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public long PurchasedQuantity { get; set; }

        public string PurchasedUnitName { get; set; }

        public long PurchasedUnitPrice { get; set; }

        public long PurchaedTotalPrice
        {
            get
            {
                return PurchasedUnitPrice * PurchasedQuantity;
            }
        }
    }
}
