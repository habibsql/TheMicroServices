namespace Sales.Query
{
    using Common.Core;
    using System;

    public class SalesQuery : IQuery
    {
        public string ProductId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
