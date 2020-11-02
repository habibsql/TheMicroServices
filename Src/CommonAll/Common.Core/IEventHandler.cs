namespace Common.Core
{
    using Common.Core.Events;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
