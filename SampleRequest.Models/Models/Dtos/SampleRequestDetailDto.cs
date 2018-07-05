using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Dtos
{
    public class SampleRequestDetailBaseDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "The quantity is required")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "The model number is required")]
        public string model_number { get; set; }

        [Required(ErrorMessage = "The description is required")]
        public string description { get; set; }

        [Required(ErrorMessage = "The specs are required")]
        public string specs { get; set; }
    }
}
