using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICustomer Customer { get; set; }
        public ICollection<IPosition> Positions { get; set; }
    }
}
