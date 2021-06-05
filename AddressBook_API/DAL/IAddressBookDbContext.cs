using AddressBook_API.Models;
using System.Collections.Generic;

namespace AddressBook_API.DAL
{
    public interface IAddressBookDbContext
    {
        IEnumerable<AddressModel> GetAllAddresses();
        AddressModel GetLastAddedAddress();
        AddressModel GetAddressById(string id);

        IEnumerable<AddressModel> GetAddressesByCityName(string cityName);
        AddressModel AddAddress(AddressModel newAddress);
        
        AddressModel EditAddress(AddressModel modifiedAddress);
        AddressModel DeleteAddress(string id);
    }
}