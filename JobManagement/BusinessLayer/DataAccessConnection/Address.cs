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
            return new AddressDto(address);
        }

        public List<AddressDto> GetAllAddresses()
        {
            var addressesList = addressRepository_.GetAllAddresses();
            return AddressDto.AddressListToAddressDtoList(addressesList);
        }

        public void AddNewAddress(AddressDto address)
        {
            addressRepository_.AddNewAddress(AddressDto.AddressDtoToAddress(address));
        }

        public void DeleteAddressByDto(AddressDto address)
        {
            addressRepository_.DeleteAddressByDto(AddressDto.AddressDtoToAddress(address));
        }

        public void UpdateAddressByDto(AddressDto address)
        {
            addressRepository_.UpdateAddressByDto(AddressDto.AddressDtoToAddress(address));
        }
    }
}
