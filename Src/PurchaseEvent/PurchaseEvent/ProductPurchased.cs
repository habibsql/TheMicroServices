namespace Purchase.Event
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Common.All;
    using Purchase.DTO;

    public class ProductPurchased : IEvent
    {
       public IList<ProductPurchasedDTO> ProductList { get; set; }
    }
}
