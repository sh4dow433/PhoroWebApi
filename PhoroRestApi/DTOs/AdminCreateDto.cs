﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DTOs
{
    public class AdminCreateDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
