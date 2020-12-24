using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class SellerCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public decimal Price { get; set; }
    }
}
