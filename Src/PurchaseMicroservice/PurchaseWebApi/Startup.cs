namespace Purchase.Api
{
    using Common.Core;
    using Common.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Purchase.Command;
    using Purchase.CommandHandler;
    using Purchase.Query;
    using Purchase.QueryHandler;
    using Purchase.Repository;
    using System;

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
            services.AddHttpClient();

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
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action}/{id?}");
            });
        }

        private void RegisterServices(IServiceCollection serviceCollection)
        {
            RegisterHelperServices(serviceCollection);
            RegisterBuses(serviceCollection);
            RegisterCommandHandlers(serviceCollection);
            RegisterQueryHandlers(serviceCollection);
            RegisterEventHanders(serviceCollection);
            RegisterRepositories(serviceCollection);
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<IPurchaseRepostiory, PurchaseRepository>();
        }

        private void RegisterCommandHandlers(IServiceCollection services)
        {
            services.AddSingleton<ICommandHandler<PurchaseCommand, CommandResult>, PurchaseCommandHandler>();
        }

        private void RegisterQueryHandlers(IServiceCollection services)
        {
            services.AddSingleton<IQueryHandler<ProductPurchasedQuery, QueryResult>, ProductPurchaseQueryHandler>();
        }

        private void RegisterEventHanders(IServiceCollection services)
        {

        }

        private void RegisterBuses(IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IQueryBus, QueryBus>();
        }

        private void RegisterHelperServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoService>(item =>
            {
                string connectionString = configRoot.GetConnectionString("default");
                return new MongoService(connectionString);
            });

            services.AddSingleton<ISerializer, JsonSerializer>();

            services.AddSingleton<IEmailService>(item =>
            {
                IConfigurationSection emailSettings = configRoot.GetSection("EmailSettings");
                var settings = new EmailSettings
                {
                    Host = emailSettings["Host"],
                    Port = int.Parse(emailSettings["Port"]),
                    UserId = emailSettings["UserId"],
                    Password = emailSettings["Password"]
                };
                return new EmailService(settings);
            });

            var rabbitMqSettings = new MessageBrokerSettings
            {
                Host = "127.0.0.1",
                Port = 5672,
                UserId = "guest",
                Password = "guest"
            };
            services.AddSingleton(rabbitMqSettings);
        }
    }
}
