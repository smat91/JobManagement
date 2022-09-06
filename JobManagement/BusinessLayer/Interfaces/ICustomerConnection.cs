using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDto = BusinessLayer.DataTransferObjects.CustomerDto;

namespace BusinessLayer.Interfaces
{
    public interface ICustomerConnection : IBaseConnection<CustomerDto>
    {
        CustomerDto GetSingleById(string customerNumber);
        void SetAddressByCustomerDtoAndAddressDto(CustomerDto customer, AddressDto address);
    }
}
