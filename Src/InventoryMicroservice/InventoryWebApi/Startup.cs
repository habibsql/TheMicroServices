namespace Inventory.Api
{
    using Common.Core;
    using Common.Core.Events;
    using Common.Infrastructure;
    using Inventory.Command;
    using Inventory.CommandHandler;
    using Inventory.EventHandler;
    using Inventory.Query;
    using Inventory.QueryHandler;
    using Inventory.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapDefaultControllerRoute();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            RegisterHelperServices(services);
            RegisterBuses(services);
            RegisterCommandHanders(services);
            RegisterQueryHandlers(services);
            RegisterEventHandlers(services);
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

        private void RegisterCommandHanders(IServiceCollection services)
        {
            services.AddSingleton<ICommandHandler<CreateStoreCommand, CommandResult>, CreateStoreCommandHandler>();
        }

        private void RegisterQueryHandlers(IServiceCollection services)
        {
            services.AddSingleton<IQueryHandler<StoreQuery, QueryResult>, StoreQueryHandler>();
        }

        private void RegisterEventHandlers(IServiceCollection services)
        {
            services.AddSingleton<IEventHandler<ProductPurchasedEvent>, ProductPurchasedEventHandler>();
            services.AddSingleton<IEventHandler<ProductSoldEvent>, ProductSoldEventHandler>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<IStoreRepository, StoreRepository>();
            services.AddSingleton<IStoreItemRepository, StoreItemRepository>();
        }

    }
}
