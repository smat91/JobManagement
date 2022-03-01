using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Customer : ICustomer
    {
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
        public string Website { get; set; }
        [Required]
        public IAddress Address { get; set; }
    }
}
