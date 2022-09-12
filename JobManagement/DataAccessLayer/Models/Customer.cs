using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        public string CustomerNumber { get; set; }
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
        public virtual Address Address { get; set; }
    }
}
