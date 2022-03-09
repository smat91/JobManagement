using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public AddressDto()
        {
        }

        public AddressDto(IAddress address)
        {
            Id = address.Id;
            Street = address.Street;
            StreetNumber = address.StreetNumber;
            Zip = address.Zip;
            Country = address.Country;
            City = address.City;
        }

        public static DataAccessLayer.Models.Address AddressDtoToAddress(AddressDto address)
        {
            return new DataAccessLayer.Models.Address
            {
                Id = address.Id,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
                Zip = address.Zip,
                Country = address.Country,
                City = address.City
            };
        }

        public static List<AddressDto> AddressListToAddressDtoList(List<IAddress> addresses)
        {
            List <AddressDto> addressDtos = new List <AddressDto>();
            foreach (var address in addresses)
            {
                addressDtos.Add(new AddressDto(address));
            }

            return addressDtos;
        }
    }
}
