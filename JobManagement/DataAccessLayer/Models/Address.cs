using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
    }
}
