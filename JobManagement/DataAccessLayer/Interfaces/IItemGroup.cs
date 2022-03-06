using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroup ParentItemGroup { get; set; }
    }
}
