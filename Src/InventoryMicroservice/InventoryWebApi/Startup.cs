namespace InventoryWebApi
{
    using Common.Core;
    using Common.Core.Events;
    using Common.Infrastructure;
    using Inventory.Command;
    using Inventory.CommandHandler;
    using Inventory.EventHandler;
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
            var rabbitMqSettings = new MessageBrokerSettings
            {
                Host = "127.0.0.1",
                Port = 5672,
                UserId = "guest",
                Password = "guest"
            };
            services.AddSingleton(rabbitMqSettings);

            services.AddSingleton<IServiceBus, RabbitMqServiceBus>();

            services.AddSingleton<ICommandHandler<CreateStoreCommand, CommandResponse>, CreateStoreCommandHandler>();

            services.AddSingleton<IEventHandler<ProductPurchasedEvent>, ProductPurchasedEventHandler>();

            services.AddSingleton<IStoreRepository, StoreRepository>();
            services.AddSingleton<IStoreItemRepository, StoreItemRepository>();

            services.AddSingleton<IMongoDbService>(new MongoDbService(configRoot.GetConnectionString("Default")));
        }
    }
}
