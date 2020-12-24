using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class PhotoCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public string ImgFile { get; set; }
    }
}
