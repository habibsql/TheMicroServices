namespace Purchase.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PurchaseQueryDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SortField { get; set; }

        /// <summary>
        /// 0 > Asending 1 > Decending
        /// </summary>
        public int SortDirection { get; set; }
    }
}
