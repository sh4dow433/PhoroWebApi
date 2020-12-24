using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class PhotoReadDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string Location { get; set; }
        public DateTime CreationDate { get; set; }

        public string ImgFile { get; set; }

    }
}
