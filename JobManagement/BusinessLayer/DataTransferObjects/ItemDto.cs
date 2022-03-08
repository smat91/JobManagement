using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
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
