namespace Purchase.Query
{
    using Common.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductPurchasedQuery : IQuery
    {
        public DateTime DateFrom { get; set; }

        public string DateTo { get; set; }

        public DateTime ProductId { get; set; }

        /// <summary>
        /// Page stared from 1
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// No of records per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Field Name to be ordered
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 0 -> Ascending 1 -> Descending 
        /// </summary>
        public int SortDirection { get; set; }
    }
}
