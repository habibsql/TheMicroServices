namespace Common.Core
{
    using System;

    public class CommandResult
    {
        public string Error { get; set; }

        public CommandResult()
        {
        }

        public CommandResult(string errorData)
        {
            this.Error = errorData;
        }

        public bool Succeed
        {
            get { return String.IsNullOrEmpty(Error); }
        }
    }
}
