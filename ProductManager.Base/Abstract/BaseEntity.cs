﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Base.Abstract
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
       
    }
}
