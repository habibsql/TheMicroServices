namespace Common.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmailParams
    {
        public List<string> ToList { get; set; }

        public List<string> CcList { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailParams()
        {
            ToList = new List<string>();
            CcList = new List<string>();
        }
    }
}
