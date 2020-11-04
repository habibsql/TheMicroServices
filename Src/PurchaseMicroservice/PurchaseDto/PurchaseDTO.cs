namespace Purchase.DTO
{
    using System;
    using System.Collections.Generic;

    public class PurchaseDTO
    {
        public DateTime PurchaseDate { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }

        public PurchaseDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }

        public long GetTotalPrice()
        {
            long total = 0;

            foreach (PurchaseItemDTO dto in PurchaseItems)
            {
                total += dto.UnitPrice * dto.Quantity;
            }

            return total;
        }
    }
}
