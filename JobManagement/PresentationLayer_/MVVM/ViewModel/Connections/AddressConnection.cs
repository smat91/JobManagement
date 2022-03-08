using System.Collections.Generic;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class AddressConnection
    {
        public static IAddress GetAddressById(int id)
        {
            var address = AddressRepository.GetAddressById(id);
            return address;
        }

        public static List<IAddress> GetAllAddresses()
        {
            var addressesList = AddressRepository.GetAllAddresses();
            return addressesList;
        }

        public static void AddNewAddress(IAddress addressDto)
        {
            AddressRepository.AddNewAddress(addressDto);
        }

        public static void DeleteAddressByDto(IAddress addressDto)
        {
            AddressRepository.DeleteAddressByDto(addressDto);
        }

        public static void UpdateAddressByDto(IAddress addressDto)
        {
            AddressRepository.UpdateAddressByDto(addressDto);
        }
    }
}
