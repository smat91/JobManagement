using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ItemGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroupDto ParentItemGroup { get; set; }
    }
}
