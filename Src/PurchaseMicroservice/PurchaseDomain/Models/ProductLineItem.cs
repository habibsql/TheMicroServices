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
        //public string PurchaseId { get; set; }

        public Product Product {get;set;}

        public int Unitrice { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; private set; }
    }
}
