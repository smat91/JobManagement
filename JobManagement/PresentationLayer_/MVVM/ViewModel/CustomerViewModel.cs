using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class CustomerViewModel
    {
        public List<CustomerDto> CustomerDtoList { get; set; }

        public CustomerViewModel()
        {
            Customer customer = new Customer();
            CustomerDtoList = customer.GetAllCustomers();
        }
    }
}
