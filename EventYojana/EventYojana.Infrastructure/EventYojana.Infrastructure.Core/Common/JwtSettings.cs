using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Common
{
    public sealed class JwtSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
