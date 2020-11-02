namespace Purchase.Domain.Model
{   
    using System;
    using Common.Core;

    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product : Entity, IAggregateRoot
    {
        public string ProductName { get; set; }

        public int UnitPrice { get; set; }

        public string UnitName { get; set; }
    }
}
