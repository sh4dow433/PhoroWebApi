using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class ReviewCreateDto
    {
        [Required]
        public int FromUserId { get; set; }

        [Required]
        public int SellerId { get; set; }

        
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public float Rating { get; set; }
    }
}
