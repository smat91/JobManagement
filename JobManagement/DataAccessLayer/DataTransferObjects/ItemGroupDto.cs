using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.DataTransferObjects
{
    public class ItemGroupDto :IItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IItemGroup ParentItemGroup { get; set; }
    }
}
