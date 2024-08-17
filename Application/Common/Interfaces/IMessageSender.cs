using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IMessageSender
    {
         Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
         Task SendEmail(string[] toEmail, string subject, string message, EmailConfig _smtpConfig, bool isMessageHtml = false);
         Task SendSmsAsync(string[] mobile, string message, SmsConfig smsConfig);
    }
}
