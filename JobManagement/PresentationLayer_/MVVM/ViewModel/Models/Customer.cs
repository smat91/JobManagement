using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Customer : ICustomer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        public DataAccessLayer.Models.Address Address { get; set; }
    }
}
