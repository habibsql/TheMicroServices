namespace Common.Core
{
    using Common.Core.Events;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventBus
    {
        Task Publish<T>(string queue, T @event) where T : IEvent;

        Task Subscribe<T>(string queue) where T : IEvent;
    }
}
