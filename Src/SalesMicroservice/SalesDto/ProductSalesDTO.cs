namespace Sales.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductSalesDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public long SalesQuantity { get; set; }
    }
}
