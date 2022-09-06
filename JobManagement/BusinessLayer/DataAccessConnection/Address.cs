using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataAccessConnection
{
    public class Address : IAddressConnection
    {
        private readonly IAddressRepository addressRepository_;

        public Address(IAddressRepository addressRepository)
        {
            addressRepository_ = addressRepository;
        }

        public AddressDto GetSingleById(int id)
        {
            var address = addressRepository_.GetSingleById(id);
            return new AddressDto(address);
        }

        public List<AddressDto> GetBySearchTerm(string searchTerm)
        {
            var addresses = addressRepository_.GetBySearchTerm(searchTerm);
            return AddressDto.AddressListToAddressDtoList(addresses);
        }

        public List<AddressDto> GetAll()
        {
            var addresses = addressRepository_.GetAll();
            return AddressDto.AddressListToAddressDtoList(addresses);
        }

        public void Add(AddressDto address)
        {
            addressRepository_.Add(AddressDto.AddressDtoToAddress(address));
        }

        public string Delete(AddressDto address)
        {
            return addressRepository_.Delete(AddressDto.AddressDtoToAddress(address));
        }

        public void Update(AddressDto address)
        {
            addressRepository_.Update(AddressDto.AddressDtoToAddress(address));
        }
    }
}
