using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Models
{
    public class AlbumPhoto
    {
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }

        public Album Album { get; set; }
        public Photo Photo { get; set; }
    }
}
