using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infrastructure
{
    public class MessageBrokerSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
