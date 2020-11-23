using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common
{
    public class PostResponseModel
    {
        public bool IsAlreadyExists { get; set; }
        public bool Success { get; set; }
        public object Content { get; set; }
    }
}
