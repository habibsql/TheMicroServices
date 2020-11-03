namespace Sales.Domain
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Sales : Aggregate
    {
        public DateTime SalesDate { get; set; }

        public IList<SalesLineItem> SalesLineItems { get; set; }

        public Sales()
        {
            this.SalesLineItems = new List<SalesLineItem>();
        }
    }
}
