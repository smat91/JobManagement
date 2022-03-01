using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ItemGroup ParentItemGroup { get; set; }
    }
}
