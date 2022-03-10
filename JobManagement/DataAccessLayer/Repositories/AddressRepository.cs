using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository
    {
        public Address GetAddressById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var address = context.Addresses.Find(id);
                return address;
            }
        }

        public List<Address> GetAddressesBySearchTerm(string searchTerm)
        {
            List<Address> addressList = new List<Address>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Addresses
                    .AsEnumerable()
                    .Where(address => search.EvaluateSearchTerm(searchTerm, address))
                    .ToList()
                    .ForEach(address => addressList.Add(address));
            }

            return addressList;
        }

        public List<Address> GetAllAddresses()
        {
            using (var context = new JobManagementContext())
            {
                List<Address> addressesList = new List<Address>();

                context.Addresses
                    .ToList()
                    .ForEach(address => addressesList.Add(address));

                return addressesList;
            }
        }

        public void AddNewAddress(Address address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Add((Address)address);
                context.SaveChanges();
            }
        }

        public void DeleteAddressByDto(Address address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Remove((Address)address);
                context.SaveChanges();
            }
        }

        public void UpdateAddressByDto(Address address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Update((Address)address);
                context.SaveChanges();
            }
        }
    }
}
