namespace Inventory.Domain
{
    using Common.Core;
    using System;

    public class StoreItem : Aggregate
    {
        public string ItemName { get; set; }

        public Store Store { get; set; }

        public long BalanceQuantity { get; set; }
    }
}
