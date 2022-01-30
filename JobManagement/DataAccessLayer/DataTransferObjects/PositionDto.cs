using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class PositionDto
    {
        public int Id { get; set; }
        public ItemDto Item { get; set; }
        public int Amount { get; set; }
    }
}
