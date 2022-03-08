using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Address
    {
        private readonly AddressRepository addressRepository_;

        public Address()
        {
            addressRepository_ = new AddressRepository();
        }

        public AddressDto GetAddressById(int id)
        {
            var address = addressRepository_.GetAddressById(id);
            return (AddressDto)address;
        }

        public List<AddressDto> GetAllAddresses()
        {
            var addressesList = addressRepository_.GetAllAddresses();
            return addressesList.ConvertAll(
                new Converter<IAddress, AddressDto>(IAddressToAddressDto));
        }

        public void AddNewAddress(AddressDto address)
        {
            addressRepository_.AddNewAddress(address);
        }

        public void DeleteAddressByDto(AddressDto address)
        {
            addressRepository_.DeleteAddressByDto(address);
        }

        public void UpdateAddressByDto(AddressDto address)
        {
            addressRepository_.UpdateAddressByDto(address);
        }

        private static AddressDto IAddressToAddressDto(IAddress address)
        {
            return (AddressDto)address;
        }
    }
}
