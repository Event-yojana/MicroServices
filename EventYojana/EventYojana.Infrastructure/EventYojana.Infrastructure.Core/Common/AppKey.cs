using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Common
{
    public sealed class AppKey
    {
        public string AppName { get; set; }
        public string KeyValue { get; set; }
    }

    public sealed class AppKeyConfig
    {
        public string HeaderName { get; set; }
        public AppKey[] AppKeys { get; set; }
    }
}
