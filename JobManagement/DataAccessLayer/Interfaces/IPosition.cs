using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    internal interface IPosition
    {
        public int Id { get; set; }
        public IItem Item { get; set; }
        public int Amount { get; set; }
    }
}
