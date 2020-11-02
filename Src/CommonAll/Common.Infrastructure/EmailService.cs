namespace Common.Infrastructure
{
    using MailKit.Net.Smtp;
    using MimeKit;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings settings;

        public EmailService(EmailSettings settings)
        {
            this.settings = settings;
        }

        public async Task SendEmail(EmailParams emailParams)
        {
            var mimeMessage = new MimeMessage();
            emailParams.ToList.ForEach(to => mimeMessage.To.Add(new MailboxAddress(to)));
            emailParams.CcList.ForEach(to => mimeMessage.Cc.Add(new MailboxAddress(to)));
            mimeMessage.From.Add(new MailboxAddress(settings.FromAddress));
            mimeMessage.Subject = emailParams.Subject;
            mimeMessage.Body = new TextPart("plain") { Text = emailParams.Body };

            using var smtpClient = new SmtpClient();
            smtpClient.Connect(settings.Host, settings.Port, settings.Ssl);
            await smtpClient.AuthenticateAsync(settings.UserId, settings.Password);

            await smtpClient.SendAsync(mimeMessage);
        }
    }
}
