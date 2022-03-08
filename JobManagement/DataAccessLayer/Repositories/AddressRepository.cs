using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository
    {
        public IAddress GetAddressById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var address = context.Addresses.Find(id);
                return address;
            }
        }

        public List<IAddress> GetAllAddresses()
        {
            using (var context = new JobManagementContext())
            {
                List<IAddress> addressesList = new List<IAddress>();

                context.Addresses
                    .ToList()
                    .ForEach(address => addressesList.Add(address));

                return addressesList;
            }
        }

        public void AddNewAddress(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Add((Address)address);
                context.SaveChanges();
            }
        }

        public void DeleteAddressByDto(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Remove((Address)address);
                context.SaveChanges();
            }
        }

        public void UpdateAddressByDto(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Update((Address)address);
                context.SaveChanges();
            }
        }
    }
}
