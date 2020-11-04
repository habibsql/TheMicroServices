namespace Common.Infrastructure
{
    using Common.Core;
    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// RabbitMQ implementaiton
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;

        public EventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            ConnectionFactory connectionFactory = CreateRabbitMqConnectionFactory();
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            consumer = new EventingBasicConsumer(channel);
        }

        public async Task Publish<T>(string queue, T @event) where T : IEvent
        {
            var serializer = serviceProvider.GetRequiredService<ISerializer>();
            string json = await serializer.Serialize(@event);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);

            await PublishInner(queue, messageBytes);
        }

        public async Task Subscribe<T>(string queue) where T : IEvent
        {
            await SubscribeInner<T>(channel, queue);
        }

        private Task PublishInner(string queue, byte[] messageBytes)
        {
            string exchangeName = CreateExchangeName(queue);
            string queueName = CreateQueueName(queue);

            channel.ExchangeDeclare(exchangeName, "fanout");
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, queueName);

            channel.BasicPublish(exchangeName, queueName, null, messageBytes);

            return Task.CompletedTask;
        }

        private Task SubscribeInner<T>(IModel channel, string queue) where T:IEvent
        {
            string queueName = CreateQueueName(queue);
            string exchangeName = CreateExchangeName(queue);

            channel.ExchangeDeclare(exchangeName, "fanout");
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, queueName);

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
