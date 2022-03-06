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
        public static IAddress GetAddressById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var address = context.Addresses.Find(id);
                return address;
            }
        }

        public static void AddNewAddress(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Add((Address)address);
                context.SaveChanges();
            }
        }

        public static void DeleteAddressByDto(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Remove((Address)address);
                context.SaveChanges();
            }
        }

        public static void UpdateAddressByDto(IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                context.Addresses.Update((Address)address);
                context.SaveChanges();
            }
        }
    }
}
