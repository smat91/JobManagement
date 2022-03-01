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
    public class OrderDto : IOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICustomer Customer { get; set; }
        public ICollection<IPosition> Positions { get; set; }
    }
}
