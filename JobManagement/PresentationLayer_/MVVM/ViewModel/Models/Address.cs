using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
