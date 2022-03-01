﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Item : IItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IItemGroup Group { get; set; }
        [Required]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
    }
}
