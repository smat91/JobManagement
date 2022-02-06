using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository
    {
        private static string ConnectionString { get; set; }

        public AddressRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public AddressDto GetAddressById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var address = context.Addresses.Find(id);

                if (address != null)
                    return new AddressDto()
                    {
                        Id = address.Id,
                        Street = address.Street,
                        StreetNumber = address.StreetNumber,
                        Zip = address.Zip,
                        Country = address.Country,
                        City = address.City
                    };
                else
                {
                    return null;
                }
            }
        }

        public void AddNewAddress(AddressDto addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Add(addressDto);
                context.SaveChanges();
            }
        }

        public void DeleteAddressByDto(AddressDto addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Remove(addressDto);
                context.SaveChanges();
            }
        }

        public void UpdateAddressByDto(AddressDto addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Update(addressDto);
                context.SaveChanges();
            }
        }
    }
}
