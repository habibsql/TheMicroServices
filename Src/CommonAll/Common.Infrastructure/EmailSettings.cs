using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infrastructure
{
    public class EmailSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public bool Ssl { get; set; }

        public string FromAddress { get; set; }
    }
}
