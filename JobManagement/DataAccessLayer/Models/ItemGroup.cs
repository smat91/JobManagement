using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class ItemGroup : IItemGroup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ItemGroup ParentItemGroup { get; set; }
    }
}
