using EventYojana.Infrastructure.Core.Interfaces;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Helpers
{
    public class MessageSenderUtility : IMessageSenderUtility
    {
        private readonly IConfiguration _configuration;

        public MessageSenderUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EmailResponse> SendEmail(string emailBody, string emailSubject, string toEmailAddress)
        {
            EmailResponse emailResponse = new EmailResponse()
            {
                ToEmailAddress = toEmailAddress,
                FromEmailAddress = _configuration.GetValue<string>("Message:Email:FromAddress"),
                IsProductionEnvironment = _configuration.GetValue<bool>("Message:IsProductionEnvironment"),
                IsEmailSend = false
            };

            string smtpAddress = _configuration.GetValue<string>("Message:Email:SmtpAddress");
            int portNumber = _configuration.GetValue<int>("Message:Email:PortNumber");
            bool enableSSL = _configuration.GetValue<bool>("Message:Email:EnableSSL");
            string password = _configuration.GetValue<string>("Message:Email:Password");

            if(!emailResponse.IsProductionEnvironment)
            {
                toEmailAddress = _configuration.GetValue<string>("Message:Email:ToAddress");
            }

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailResponse.FromEmailAddress);
                    mail.To.Add(toEmailAddress);
                    mail.Subject = emailSubject;
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailResponse.FromEmailAddress, password);
                        smtp.EnableSsl = enableSSL;
                        await smtp.SendMailAsync(mail);
                    }
                }

                emailResponse.IsEmailSend = true;
            }
            catch(Exception ex)
            {
                emailResponse.IsEmailSend = false;
            }
            
            return emailResponse;
        }
    }
}
