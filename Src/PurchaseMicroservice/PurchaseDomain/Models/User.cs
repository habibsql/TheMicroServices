namespace Purchase.Domain.Model
{
    using Common.Core;

    public class User : Entity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
