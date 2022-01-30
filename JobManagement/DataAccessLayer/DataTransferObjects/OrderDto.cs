using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<PositionDto> Positions { get; set; }
    }
}
