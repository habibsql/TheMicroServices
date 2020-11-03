namespace Common.Infrastructure
{
    using Common.Core;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> Send<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : QueryResult
        {
            var queryHandler = serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

            return await queryHandler.Handle(query);
        }
    }
}

