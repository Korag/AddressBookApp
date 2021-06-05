using AddressBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook_API.DAL
{
    public class MemoryAddressBookDbContext : IAddressBookDbContext
    {
        public static List<AddressModel> addressBook { get; private set; } = SeedAddressBook();

        public AddressModel AddAddress(AddressModel newAddress)
        {
            addressBook.Add(newAddress);
            return GetAddressById(newAddress.Id);
        }

        public AddressModel GetLastAddedAddress()
        {
            return addressBook.LastOrDefault();
        }

        public AddressModel GetAddressById(string id)
        {
            return addressBook.Where(z => z.Id.ToUpper() == id.ToUpper()).FirstOrDefault();
        }

        public IEnumerable<AddressModel> GetAddressesByCityName(string cityName)
        {
            if (!String.IsNullOrWhiteSpace(cityName))
            {
                return addressBook.Where(z => z.City.ToUpper() == cityName.ToUpper()).ToList();
            }

            return new List<AddressModel>();
        }

        public IEnumerable<AddressModel> GetAllAddresses()
        {
            return addressBook;
        }

        public AddressModel EditAddress(AddressModel modifiedAddress)
        {
            AddressModel addressToEdit = GetAddressById(modifiedAddress.Id);

            addressToEdit.FirstName = modifiedAddress.FirstName;
            addressToEdit.LastName = modifiedAddress.LastName;
            addressToEdit.City = modifiedAddress.City;
            addressToEdit.Street = modifiedAddress.Street;
            addressToEdit.PostCode = modifiedAddress.PostCode;
            addressToEdit.ApartmentNumber = modifiedAddress.ApartmentNumber;
            addressToEdit.PhoneNumber = modifiedAddress.PhoneNumber;

            return modifiedAddress;
        }

        public AddressModel DeleteAddress(string id)
        {
            AddressModel addressToRemove = GetAddressById(id);
            addressBook.Remove(addressToRemove);

            return addressToRemove;
        }

        #region SeedDatabaseContent
        public static List<AddressModel> SeedAddressBook()
        {
            List<AddressModel> addressBook = new List<AddressModel>()
            { new AddressModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    City = "Bielsko-Biała",
                    Street = "Test",
                    PostCode = "43-300",
                    ApartmentNumber = "1",
                    PhoneNumber = "123456789"
                },
                new AddressModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Tadeusz",
                    LastName = "Nowak",
                    City = "Bielsko-Biała",
                    Street = "Scenariusz",
                    PostCode = "43-300",
                    ApartmentNumber = "4/43",
                    PhoneNumber = "987654321"
                },
                new AddressModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Rafał",
                    LastName = "XYZ",
                    City = "Katowice",
                    Street = "Śródmiejska",
                    PostCode = "40-000",
                    ApartmentNumber = "21",
                    PhoneNumber = "745691258"
                },
            };

            return addressBook;
        }
        #endregion
    }
}