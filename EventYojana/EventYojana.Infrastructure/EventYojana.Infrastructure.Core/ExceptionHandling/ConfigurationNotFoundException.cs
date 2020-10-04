using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.ExceptionHandling
{
    public sealed class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException()
        {}

        public ConfigurationNotFoundException(string message): base(message)
        {}

        public ConfigurationNotFoundException(string message, Exception exception) : base(message, exception)
        {}
    }
}
