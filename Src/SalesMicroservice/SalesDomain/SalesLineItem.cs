namespace Sales.Domain
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SalesLineItem 
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string UnitName { get; set; }

        public long UnitSalePrice { get; set; }

        public long SalesQuantity { get; set; }

        public long SalesTotalPrice
        {
            get { return UnitSalePrice * SalesQuantity; }
        }
    }
}
