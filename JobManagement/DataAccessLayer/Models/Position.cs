using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PositionDto
    {
        public int Id { get; set; }
        [Required]
        public virtual ItemDto Item { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
