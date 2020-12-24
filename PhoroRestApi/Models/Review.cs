using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Models
{
    public class Review
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int FromUserId { get; set; }

        [Required]
        public int SellerId { get; set; }

        [Required]
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; }


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
