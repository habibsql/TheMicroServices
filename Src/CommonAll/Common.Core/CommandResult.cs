namespace Common.Core
{
    using System;

    public class CommandResult
    {
        public string ErrorData { get; set; }

        public CommandResult()
        {
        }

        public CommandResult(string errorData)
        {
            this.ErrorData = errorData;
        }

        public bool Succeed
        {
            get { return String.IsNullOrEmpty(ErrorData); }
        }
    }
}
