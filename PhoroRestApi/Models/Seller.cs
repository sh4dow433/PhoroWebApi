using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Models
{
    public class Seller
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }


        public int ProfilePicId { get; set; }
        public int CoverPicId { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        public float Rating 
        {
            get
            {
                if (Reviews == null)
                {
                    return 0;
                }
                if (Reviews.Count >= 1)
                {
                    return Reviews.Average(r => r.Rating);
                } 
                else
                {
                    return 0;
                }
            }
            private set { } 
        }
        public decimal Price { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }


    }
}
