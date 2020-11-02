namespace Purchase.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BusinessException : Exception
    {
        public BusinessException():base()
        {            
        }
        public BusinessException(string  message) : base(message)
        {
        }
    }
}
