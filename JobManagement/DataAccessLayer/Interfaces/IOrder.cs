using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}
