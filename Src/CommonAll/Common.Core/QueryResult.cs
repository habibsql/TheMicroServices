namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class QueryResult
    {
        public string Error { get; set; }
        public object Result { get; set; }

        public bool Succeed => string.IsNullOrEmpty(Error);
    }
}
