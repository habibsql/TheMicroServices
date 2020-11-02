namespace Purchase.Command
{
    using Common.Core;
    using System;
    using System.Collections.Generic;

    public class PurchaseCommand : ICommand
    {
        public DateTime PurchaseDate { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public IList<LineItemCommand> LineItems { get; set; }
    }
}
