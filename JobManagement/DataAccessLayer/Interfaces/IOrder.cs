using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    internal interface IOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual ICustomer Customer { get; set; }
        public virtual ICollection<IPosition> Positions { get; set; }
    }
}
