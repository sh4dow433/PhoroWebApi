using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Models
{
    public class Album
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int SellerId { get; set; }


        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }


        // public List<Photo> Photos { get; set; } = new List<Photo>();
        public IList<AlbumPhoto> AlbumPhotos { get; set; } = new List<AlbumPhoto>();

        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; }

    }
}
