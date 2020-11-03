namespace Common.Core.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductSoldEvent : IEvent
    {
        public string SalesId { get; set; }

        public DateTime SalesDate { get; set; }

        public IList<ProudctSoldLineItem> ProductSoldLineItems { get; set; }

        public ProductSoldEvent()
        {
            ProductSoldLineItems = new List<ProudctSoldLineItem>();
        }
    }

    /// <summary>
    /// Part of ProductSoldEvent
    /// </summary>
    public class ProudctSoldLineItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public long SoldQuantity { get; set; }

        public string SoldUnitName { get; set; }

        public long SoldUnitPrice { get; set; }

        public long SoldTotalPrice
        {
            get
            {
                return SoldUnitPrice * SoldQuantity;
            }
        }
    }
}
