namespace Purchase.Query
{
    using Common.Core;
    using System;

    public class ProductPurchasedQuery : IQuery
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

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
        public string SortFiled { get; set; }

        /// <summary>
        /// 0 -> Ascending 1 -> Descending 
        /// </summary>
        public int SortDirection { get; set; }
    }
}
