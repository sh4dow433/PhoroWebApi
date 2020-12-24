using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int FromUserId { get; set; }
        
        [Required]
        public int ToUserId { get; set; }


        [Required]
        [MaxLength(500)]
        public string Content { get; set; }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; }
    }
}
