using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository
    {
        private static string connectionString_;

        public AddressRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public IAddress GetAddressById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var address = context.Addresses.Find(id);
                return address;
            }
        }

        public void AddNewAddress(IAddress addressDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Addresses.Add((Address)addressDto);
                context.SaveChanges();
            }
        }

        public void DeleteAddressByDto(IAddress addressDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Addresses.Remove((Address)addressDto);
                context.SaveChanges();
            }
        }

        public void UpdateAddressByDto(IAddress addressDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Addresses.Update((Address)addressDto);
                context.SaveChanges();
            }
        }
    }
}
