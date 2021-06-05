using FluentValidation;

namespace AddressBook_API.DTO
{
    public class EditAddressDTO
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

    public class EditAddressValidator : AbstractValidator<EditAddressDTO>
    {
        public EditAddressValidator()
        {
            RuleFor(x => x.Id).NotNull().Length(36);
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