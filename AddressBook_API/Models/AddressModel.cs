namespace AddressBook_API.Models
{
    public class AddressModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
