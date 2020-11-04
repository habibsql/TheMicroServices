namespace Inventory.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Constants
    {
        public class MessageQueue
        {
            public const string PurchaseQueue = "product-purchased";
            public const string SalesQueue = "product-sales";
        }
    }
}
