using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IItemGroup Group { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
    }
}
