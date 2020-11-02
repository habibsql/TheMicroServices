namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<T> where T: IAggregateRoot
    {
        Task<T> Save(T entity);

        Task<T> GetById(string id);
    }
}
