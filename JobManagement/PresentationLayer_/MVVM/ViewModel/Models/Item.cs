using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Item : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataAccessLayer.Models.ItemGroup Group { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
    }
}
