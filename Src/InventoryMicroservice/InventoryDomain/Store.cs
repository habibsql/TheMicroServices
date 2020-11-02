namespace Inventory.Domain
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Store : Entity, IAggregateRoot
    {
        public string Manager { get; set; }
    }
}
