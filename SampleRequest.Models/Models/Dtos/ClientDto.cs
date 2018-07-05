using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class ClientBaseDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class ClientDto : ClientBaseDto
    {
        public virtual List<AddressBaseDto> addresses { get; set; }
    }
}
