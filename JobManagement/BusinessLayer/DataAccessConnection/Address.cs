using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
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
            var address = addressRepository_.GetSingleById(id);
            return new AddressDto(address);
        }

        public List<AddressDto> GetAddressesBySearchTerm(string searchTerm)
        {
            var addresses = addressRepository_.GetBySearchTerm(searchTerm);
            return AddressDto.AddressListToAddressDtoList(addresses);
        }

        public List<AddressDto> GetAllAddresses()
        {
            var addresses = addressRepository_.GetAll();
            return AddressDto.AddressListToAddressDtoList(addresses);
        }

        public void AddNewAddress(AddressDto address)
        {
            addressRepository_.Add(AddressDto.AddressDtoToAddress(address));
        }

        public string DeleteAddressByDto(AddressDto address)
        {
            return addressRepository_.Delete(AddressDto.AddressDtoToAddress(address));
        }

        public void UpdateAddressByDto(AddressDto address)
        {
            addressRepository_.Update(AddressDto.AddressDtoToAddress(address));
        }
    }
}
