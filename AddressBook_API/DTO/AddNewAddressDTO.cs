using FluentValidation;

namespace AddressBook_API.DTO
{
    public class AddNewAddressDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class AddNewAddressValidator : AbstractValidator<AddNewAddressDTO>
    {
        public AddNewAddressValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.Street).NotNull();
            RuleFor(x => x.ApartmentNumber).NotNull();
            RuleFor(x => x.PostCode).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull();
        }
    }
}
