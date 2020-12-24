using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class SellerReadInfoDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int ProfilePicId { get; set; }

        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }

        public float Rating { get; set; }
        public decimal Price { get; set; }
    }
}
