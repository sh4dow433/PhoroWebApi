using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class SellerReadFullDto : SellerReadInfoDto
    {
        public List<ReviewReadDto> Reviews { get; set; }
        public List<AlbumReadDto> Albums { get; set; }
        public int CoverPicId { get; set; }
        //public List<int> PhotosIds { get; set; }
    }
}
