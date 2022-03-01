using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ItemGroup Group { get; set; }
        public decimal Price { get; set; }
    }
}
