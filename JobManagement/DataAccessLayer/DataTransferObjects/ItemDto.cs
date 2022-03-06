using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.DataTransferObjects
{
    public class ItemDto : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroup Group { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
    }
}
