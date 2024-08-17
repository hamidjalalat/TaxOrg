using Application.Common.Interfaces;
using Application.Common.Models;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Infrastructure.Common
{
    public class MessageSender : IMessageSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            using (var client = new SmtpClient())
            {

                var credentials = new NetworkCredential()
                {
                    UserName = "saeed.moradi3", // without @gmail.com
                    Password = ""
                };

                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress("saeed.moradi3"), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                await client.SendMailAsync(emailMessage);
            }

            //return Task.CompletedTask;
        }

        public async Task SendEmail(string[] toEmail, string subject, string message, EmailConfig _smtpConfig, bool isMessageHtml = false)
        {
            MailMessage mail = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = isMessageHtml
            };

            foreach (var email in toEmail)
            {
                mail.To.Add(email);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,

                Credentials = networkCredential,
                EnableSsl = _smtpConfig.EnableSSL

            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendSmsAsync(string[] mobile, string message,SmsConfig smsConfig)
        {
            
            try
            {
                //Ghasedak.Core.Api sms = new Ghasedak.Core.Api(smsConfig.ApiKey);
                //var res = await sms.SendSMSAsync(message, mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
