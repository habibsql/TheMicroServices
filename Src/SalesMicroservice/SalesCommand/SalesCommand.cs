namespace Sales.Command
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SalesCommand : ICommand
    {
        public string SalesId { get; set; }

        public DateTime SalesDate { get; set; }

        public IList<SalesProduct> SalesProducts { get; set; }

        public SalesCommand()
        {
            this.SalesProducts = new List<SalesProduct>();
        }
    }

    public class SalesProduct
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string UnitName { get; set; }

        public long SalesUnitPrice { get; set; }

        public long SalesQuantity { get; set; }
    }
}
