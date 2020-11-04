namespace Purchase.Domain.Model
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Product Purchase Line Item Entity
    /// </summary>
    public class ProductLineItem : Entity
    {
        public Product Product { get; set; }

        public long PurchaseUnitPrice { get; set; }

        public long PurchaseQuantity { get; set; }

        public long PurchaseTotalPrice
        {
            get
            {
                return PurchaseQuantity * PurchaseUnitPrice;
            }
        }
    }
}
