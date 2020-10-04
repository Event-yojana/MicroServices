using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Attributes
{
    public class ConsumesFileAttribute : ConsumesAttribute
    {
        public ConsumesFileAttribute() : base("multipart/form-data", "application/json") { }
    }
}
