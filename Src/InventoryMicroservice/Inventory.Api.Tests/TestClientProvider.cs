namespace Inventory.Api.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;

    public class TestClientProvider
    {
        public HttpClient HttpClient { get; private set; }

        public TestClientProvider()
        {
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseConfiguration(GetConfig());
            webHostBuilder.UseStartup<Startup>();

            var testServer = new TestServer(webHostBuilder);
            HttpClient = testServer.CreateClient();
        }

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
