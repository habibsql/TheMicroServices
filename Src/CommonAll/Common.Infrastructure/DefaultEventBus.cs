namespace Common.Infrastructure
{
    using Common.Core;
    using Common.Core.Events;
    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// RabbitMQ implementaiton
    /// </summary>
    public class DefaultEventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultEventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Publish<T>(string queue, T @event) where T : IEvent
        {
            var serializer = serviceProvider.GetService<ISerializer>();
            string json = await serializer.Serialize(@event);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);

            await PublishInner(queue, messageBytes);
        }

        public async Task Subscribe<T>(string queue) where T : IEvent
        {
            ConnectionFactory connectionFactory = CreateRabbitMqConnectionFactory();
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            await SubscribeInner<T>(channel, queue);
        }

        private Task PublishInner(string queueName, byte[] messageBytes)
        {
            ConnectionFactory connectionFactory = CreateRabbitMqConnectionFactory();
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string exchangeName = CreateExchangeName(queueName);
            string modifiedQueueName = CreateQueueName(queueName);

            channel.ExchangeDeclare(exchangeName, "fanout");
            channel.QueueDeclare(modifiedQueueName, true, false, false, null);
            channel.QueueBind(modifiedQueueName, exchangeName, modifiedQueueName);

            channel.BasicPublish(exchangeName, modifiedQueueName, null, messageBytes);

            return Task.CompletedTask;
        }

        private Task SubscribeInner<T>(IModel channel, string queue) where T:IEvent
        {
            var consumer = new EventingBasicConsumer(channel);
            string queueName = CreateQueueName(queue);

            consumer.Received += async (sender, e) =>
            {
                byte[] bytes = e.Body.ToArray();
                string messageJson = Encoding.UTF8.GetString(bytes);
                var serializer = serviceProvider.GetRequiredService<ISerializer>();
                T messageObject = await serializer.Decerialize<T>(messageJson);
                var eventHandler = serviceProvider.GetService<IEventHandler<T>>();

                await eventHandler.Handle(messageObject);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private ConnectionFactory CreateRabbitMqConnectionFactory()
        {
            var settings = serviceProvider.GetRequiredService<MessageBrokerSettings>();

            return new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.UserId,
                Password = settings.Password
            };
        }

        /// <summary>
        /// Modifed Queue name
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        private string CreateQueueName(string queueName)
        {
            return $"{queueName}-queue";
        }

        /// <summary>
        /// Create exchange name from QueueName 
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        private string CreateExchangeName(string queueName)
        {
            return $"{queueName}-exchange";
        }

    }
}
