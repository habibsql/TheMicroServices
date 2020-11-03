namespace SalesWebApi
{
    using Common.Core;
    using Common.Core.Events;
    using Common.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Sales.Command;
    using Sales.CommandHandler;
    using Sales.Query;
    using Sales.QueryHandler;
    using Sales.Repository;

    public class Startup
    {
        private readonly IConfiguration configRoot;

        public Startup(IConfiguration configRoot)
        {
            this.configRoot = configRoot;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            RegisterHelperServices(services);
            RegisterBuses(services);
            RegisterCommandHandlers(services);
            RegisterEventHandlers(services);
            RegisterQueryHandlers(services);
            RegisterRepositories(services);
        }

        private void RegisterHelperServices(IServiceCollection services)
        {
            var rabbitMqSettings = new MessageBrokerSettings
            {
                Host = "127.0.0.1",
                Port = 5672,
                UserId = "guest",
                Password = "guest"
            };
            services.AddSingleton(rabbitMqSettings);
            services.AddSingleton<IMongoService>(new MongoService(configRoot.GetConnectionString("Default")));
        }

        private void RegisterBuses(IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IQueryBus, QueryBus>();
        }

        private void RegisterCommandHandlers(IServiceCollection services)
        {
            services.AddSingleton<ICommandHandler<SalesCommand, CommandResult>, SalesCommandHandler>();
        }

        private void RegisterEventHandlers(IServiceCollection services)
        {
        }

        private void RegisterQueryHandlers(IServiceCollection services)
        {
            services.AddSingleton<IQueryHandler<SalesQuery, QueryResult>, SalesQueryHandler>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<ISalesRepository, SalesRepository>();
        }
    }
}
