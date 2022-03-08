﻿using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataTransferObjects
{
    public class AddressDto : IAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
