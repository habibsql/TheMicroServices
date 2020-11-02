namespace Inventory.Domain
{
    using Common.Core;
    using System;

    public class StoreItem : Aggregate
    {
        public string ItemName { get; set; }

        public string UnitName { get; set; }

        public Store Store { get; set; }

        public string PurchaseId { get; set; }

        public int Quantity { get; set; }
    }
}
