namespace Inventory.Domain
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Store : Aggregate
    {
        public string Manager { get; set; }
    }
}
