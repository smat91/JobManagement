using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetSingleById(int id);
        List<Customer> GetAll(DateTime date);
        void SetAddressByCustomerAndAddress(Customer customer, Address address);
        void ImportCustomers(string filePath, string fileType);
        void ExportCustomers(string filePath, string fileType, DateTime date);
    }
}
