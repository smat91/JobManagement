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
    public class PositionDto : IPosition
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }
}
