namespace Purchase.Domain.Model
{   
    using System;
    using Common.Core;

    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product : Aggregate
    {
        public string ProductName { get; set; }
    }
}
