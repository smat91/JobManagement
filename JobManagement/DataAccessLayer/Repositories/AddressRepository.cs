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
        private static string ConnectionString { get; set; }

        public AddressRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IAddress GetAddressById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var address = context.Addresses.Find(id);
                return address;
            }
        }

        public void AddNewAddress(IAddress addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Add((Address)addressDto);
                context.SaveChanges();
            }
        }

        public void DeleteAddressByDto(IAddress addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Remove((Address)addressDto);
                context.SaveChanges();
            }
        }

        public void UpdateAddressByDto(IAddress addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Addresses.Update((Address)addressDto);
                context.SaveChanges();
            }
        }
    }
}
