namespace Common.Infrastructure.Tests
{
    using Common.Core;
    using Common.Core.Events;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Moq.Language.Flow;
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class RabbitMqServiceBusTest
    {
        private readonly MessageBrokerSettings rabbitMqSettings;
        private readonly RabbitMqServiceBus rabbitMqServiceBus;
        private const string QueueName = "product-purchased";

        public RabbitMqServiceBusTest()
        {
            rabbitMqSettings = new MessageBrokerSettings
            {
                Host = "127.0.0.1",
                Port = 5672,
                UserId = "guest",
                Password = "guest"
            };

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(srv => srv.GetService(typeof(MessageBrokerSettings))).Returns(rabbitMqSettings);
            serviceProvider.Setup(srv => srv.GetService(typeof(ISerializer))).Returns(new JsonSerializer());
            serviceProvider.Setup(item => item.GetService(typeof(IEventHandler<ProductPurchasedEvent>)))
             .Returns(new PurchaseEventHandler());

            rabbitMqServiceBus = new RabbitMqServiceBus(serviceProvider.Object);
        }

        [Fact]
        public async Task ShouldPublishMessageWhenValidQueueProvided()
        {
            var lineItems = new List<LineItem>
            {
                new LineItem{ProductId = Guid.NewGuid().ToString(), PricePerUnit = 100, Quantity = 100},
                new LineItem{ProductId = Guid.NewGuid().ToString(), PricePerUnit = 200, Quantity = 200},
            };

            var productPurchasedEvent = new ProductPurchasedEvent
            {
                PurchaseDate = DateTime.UtcNow,
                LineItems = lineItems
            };

            await rabbitMqServiceBus.Publish(QueueName, productPurchasedEvent);
        }

        [Fact]
        public async Task ShouldReceiveMessageWhenValidQueueProvided()
        {
            await rabbitMqServiceBus.Subscribe<ProductPurchasedEvent>(QueueName);

            await Task.Delay(1000 * 3);
        }

        private IConnection CreateRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                Port = rabbitMqSettings.Port,
                UserName = rabbitMqSettings.UserId,
                Password = rabbitMqSettings.Password
            };

            return connectionFactory.CreateConnection();
        }
    }

    public class PurchaseEventHandler : IEventHandler<ProductPurchasedEvent>
    {
        public async Task Handle(ProductPurchasedEvent @event)
        {
            @event.Should().NotBeNull();

           await Task.CompletedTask;
        }
    }
}
