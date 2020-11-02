namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQueryHandler<T1, T2> where T1 : IQuery
                                           where T2 : QueryResult
    {

        public Task<T2> Handle(T1 query);
    }
}
