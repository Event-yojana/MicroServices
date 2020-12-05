using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
    public class EmailLogs
    {
        public int EmailLogId { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsProduction { get; set; }
        public bool IsSend { get; set; }
        public int ApplicationId { get; set; }
        public string FromUserType { get; set; }
        public int? FromUserId { get; set; }
        public string ToUserType { get; set; }
        public int? ToUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
