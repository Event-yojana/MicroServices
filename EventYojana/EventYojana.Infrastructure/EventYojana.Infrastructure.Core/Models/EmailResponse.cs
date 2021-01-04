using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Models
{
    public class EmailResponse
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public bool IsEmailSend { get; set; }
        public bool IsProductionEnvironment { get; set; }

    }
}
