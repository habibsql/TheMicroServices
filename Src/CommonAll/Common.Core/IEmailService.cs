namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendEmail(EmailParams emailParams);
    }
}
