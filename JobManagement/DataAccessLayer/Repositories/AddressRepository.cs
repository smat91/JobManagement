using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

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
                context.Addresses.Add(address);
                context.SaveChanges();
            }
        }

        public string DeleteAddressByDto(Address address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Remove(address);

                try
                {
                    context.SaveChanges();
                    return "Datensatz erfolgreich gelöscht";
                }
                catch (DbUpdateException e)
                {
                    return "Datensatz konnte nicht gelöscht werden.\nBitte zuerst Datensätze erntfernen in denen der Datensatz verwendet wird.";
                }
            }
        }

        public void UpdateAddressByDto(Address address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Update(address);
                context.SaveChanges();
            }
        }
    }
}
