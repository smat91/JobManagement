using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces.Helper;
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
        void SetAddressByCustomerDtoAndAddressDto(CustomerDto customer, AddressDto address);
        void ImportCustomers(string filePath, string fileType);
        void ExportCustomers(string filePath, string fileType, DateTime date);

    }
}
