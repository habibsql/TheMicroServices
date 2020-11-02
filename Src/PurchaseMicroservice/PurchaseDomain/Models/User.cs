namespace Purchase.Domain.Model
{
    using Common.Core;

    public class User : Aggregate
    {
        public string Name { get; set; }
    }
}
