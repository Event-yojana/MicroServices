using EventYojana.Infrastructure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Interfaces
{
    public interface IMessageSenderUtility
    {
        Task<EmailResponse> SendEmail(string emailBody, string emailSubject, string toEmailAddress);
    }
}
