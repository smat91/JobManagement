using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public virtual Item Item { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
