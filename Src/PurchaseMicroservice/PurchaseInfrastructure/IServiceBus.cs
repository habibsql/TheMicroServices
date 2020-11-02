namespace Purchase.Infrastructure
{
    using Common.All;
    using System;
    using System.Threading.Tasks;

    public interface IServiceBus
    {
        Task Publish(string message, string queue);
    }
}
