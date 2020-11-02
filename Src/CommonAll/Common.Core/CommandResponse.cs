namespace Common.Core
{
    using System;

    public class CommandResponse
    {
        public string ErrorData { get; set; }

        public CommandResponse()
        {
        }

        public CommandResponse(string errorData)
        {
            this.ErrorData = errorData;
        }

        public bool Succeed
        {
            get { return String.IsNullOrEmpty(ErrorData); }
        }
    }
}
