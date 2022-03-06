using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class AddressConnection
    {
        private readonly AddressRepository addressRepository_;

        public AddressConnection(string connectionString)
        {
            addressRepository_ = new AddressRepository(connectionString);
        }

        public IAddress GetAddressById(int id)
        {
            var address = addressRepository_.GetAddressById(id);
            return address;
        }

        public void AddNewAddress(IAddress addressDto)
        {
            addressRepository_.AddNewAddress(addressDto);
        }

        public void DeleteAddressByDto(IAddress addressDto)
        {
            addressRepository_.DeleteAddressByDto(addressDto);
        }

        public void UpdateAddressByDto(IAddress addressDto)
        {
            addressRepository_.UpdateAddressByDto(addressDto);
        }
    }
}
