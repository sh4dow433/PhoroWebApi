using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class ReviewReadDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int SellerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
