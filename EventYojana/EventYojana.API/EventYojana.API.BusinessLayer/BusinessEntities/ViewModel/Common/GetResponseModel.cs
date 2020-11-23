using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common
{
    public class GetResponseModel
    {
        public bool NoContent { get; set; }
        public bool Success { get; set; }
        public object Content { get; set; }
    }
}
