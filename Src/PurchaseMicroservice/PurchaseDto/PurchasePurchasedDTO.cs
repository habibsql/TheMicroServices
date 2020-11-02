namespace Purchase.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PurchasePurchasedDTO
    {
        public DateTime Date { get; set; }

        public string ProductName { get; set; }

        public long PurchasedQuantity { get; set; }

        public long PurchasedAmount { get; set; }
    }
}
