using System.ComponentModel.DataAnnotations;

namespace SamplesRequest.Dtos
{
    public class AddressBaseDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "The street and number are required on the shipping info")]
        public string street_number { get; set; }

        [Required(ErrorMessage = "The zip number is required on the shipping info")]
        public string zip { get; set; }

        [Required(ErrorMessage = "The faclity is required on the shipping info ")]
        public string facility { get; set; }

        [Required(ErrorMessage = "")]
        public string phone { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public int client_id { get; set; }
    }

    public class AddressDto : AddressBaseDto
    {
        public virtual ClientBaseDto client { get; set; }
    }
}