namespace Purchase.Infrastructure
{
    using Common.All;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using RabbitMQ.Client;
    using System.Text.Json;

    public class RabbitMqBus : IServiceBus
    {
        private readonly IDictionary<string, string> settings;

        public RabbitMqBus(IDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        public Task Publish(string message, string queue)
        { 
            PublishLocal(message, queue);

            return Task.CompletedTask;
        }

        private void PublishLocal(string message, string queue)
        {
            var factory = new ConnectionFactory
            {
                HostName = settings["host"],
                Port = Convert.ToInt32(settings["port"])
            };
            using var connection = factory.CreateConnection();
            using var model = connection.CreateModel();

            QueueDeclareOk ok = model.QueueDeclare(queue, true, false, false, null);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(message);

            model.BasicPublish(string.Empty, queue, null, bodyBytes);
        }
    }
}
