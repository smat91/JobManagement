using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class AddressConnection
    {
        public static IAddress GetAddressById(int id)
        {
            var address = AddressRepository.GetAddressById(id);
            return address;
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
