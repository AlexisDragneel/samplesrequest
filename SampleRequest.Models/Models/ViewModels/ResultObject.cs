using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.ViewModels
{
    public class ResultObject
    {
        public bool success { get; set; }

        public List<string> messages { get; set; }

        public object data { get; set; }
    }
}
