using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ICustomer Customer { get; set; }
        [Required]
        public ICollection<Position> Positions { get; set; }
    }
}
