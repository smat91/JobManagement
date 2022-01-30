using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ItemGroupDto Group { get; set; }
        [Required]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
    }
}
