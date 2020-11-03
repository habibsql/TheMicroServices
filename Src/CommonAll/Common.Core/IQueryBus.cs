namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQueryBus
    {
        Task<TResult> Send<TQuery, TResult>(TQuery query) where TQuery : IQuery
                                                                     where TResult : QueryResult;
    }
}
