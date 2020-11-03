namespace Common.Core
{
    using System.Threading.Tasks;

    public interface IQueryHandler<TQuery, TResult> 
        where TQuery : IQuery
        where TResult : QueryResult
    {
        public Task<TResult> Handle(TQuery query);
    }
}
