using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnSecureheaderAttribute : Attribute
    {
    }
}
