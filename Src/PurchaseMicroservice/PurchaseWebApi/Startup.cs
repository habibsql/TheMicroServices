namespace PurchaseWebApi
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
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action}/{id?}");
            });
        }

        private void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICommandBus, CommandBus>();
            serviceCollection.AddSingleton<IServiceBus, IServiceBus>();
            serviceCollection.AddSingleton<ICommandHandler<PurchaseCommand, CommandResponse>, PurchaseCommandHandler>();
            serviceCollection.AddSingleton<IMongoDbService>(item =>
            {
                string connectionString = configRoot.GetConnectionString("mongodb");
                return new MongoDbService(connectionString);
            });
            serviceCollection.AddSingleton<ISerializer, JsonSerializer>();
            serviceCollection.AddSingleton<IEmailService>(item =>
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
        }
    }
}
