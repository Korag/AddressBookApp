using AddressBook_API.DAL;
using AddressBook_API.DTO;
using AddressBook_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AddressBook_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly ILogger<AddressBookController> _logger;
        private readonly IAddressBookDbContext _context;

        public AddressBookController(ILogger<AddressBookController> logger,
                                     IAddressBookDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("GetAddressesByCityName/{cityName}")]
        [HttpGet]
        public IActionResult GetAddressesByCityName(string cityName)
        {
            LogRequestPath();

            if (!String.IsNullOrWhiteSpace(cityName))
            {
                IEnumerable<AddressModel> addressesList = _context.GetAddressesByCityName(cityName);

                LogInformationRequestResponse(Ok());
                return Ok(addressesList);
            }
            else
            {
                ModelState.AddModelError("", "The attribute cityName cannot be null or whitespace");

                LogWarningRequestResponse(BadRequest(), "Model state is not valid");
                return BadRequest(ModelState);
            }
        }

        [Route("GetLastAddedAddress")]
        [HttpGet]
        public IActionResult GetLastAddedAddress()
        {
            LogRequestPath();
            AddressModel lastAddedAddress = _context.GetLastAddedAddress();

            LogInformationRequestResponse(Ok());
            return Ok(lastAddedAddress);
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAddressBook()
        {
            LogRequestPath();
            IEnumerable<AddressModel> addressBook = _context.GetAllAddresses();

            LogInformationRequestResponse(Ok());
            return Ok(addressBook);
        }

        [Route("AddNewAddress")]
        [HttpPost]
        public IActionResult AddNewAddress(AddNewAddressDTO addressToAdd)
        {
            LogRequestPath();

            if (ModelState.IsValid)
            {
                AddressModel newAddress = new AddressModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = addressToAdd.FirstName,
                    LastName = addressToAdd.LastName,
                    City = addressToAdd.City,
                    Street = addressToAdd.Street,
                    ApartmentNumber = addressToAdd.ApartmentNumber,
                    PostCode = addressToAdd.PostCode,
                    PhoneNumber = addressToAdd.PhoneNumber
                };

                AddressModel createdAddress = _context.AddAddress(newAddress);

                LogInformationRequestResponse(Ok());
                return Ok(createdAddress);
            }
            else
            {
                LogWarningRequestResponse(BadRequest(), "Model state is not valid");
                return BadRequest(ModelState);
            }
        }

        [Route("EditAddress")]
        [HttpPut]
        public IActionResult EditAddress(EditAddressDTO addressToEdit)
        {
            LogRequestPath();

            if (ModelState.IsValid)
            {
                AddressModel newAddress = new AddressModel()
                {
                    Id = addressToEdit.Id,
                    FirstName = addressToEdit.FirstName,
                    LastName = addressToEdit.LastName,
                    City = addressToEdit.City,
                    Street = addressToEdit.Street,
                    ApartmentNumber = addressToEdit.ApartmentNumber,
                    PostCode = addressToEdit.PostCode,
                    PhoneNumber = addressToEdit.PhoneNumber
                };

                AddressModel editedAddress = _context.EditAddress(newAddress);

                if (editedAddress != null)
                {
                    LogInformationRequestResponse(Ok());
                    return Ok(editedAddress);
                }
                else
                {
                    LogWarningRequestResponse(NotFound(), "Address not found in database");
                    return NotFound();
                }
            }
            else
            {
                LogWarningRequestResponse(BadRequest(), "Model state is not valid");
                return BadRequest(ModelState);
            }
        }

        [Route("DeleteAddress/{id}")]
        [HttpDelete]
        public IActionResult DeleteAddress(string id)
        {
            LogRequestPath();

            if (!String.IsNullOrWhiteSpace(id))
            {
                AddressModel deletedAddress = _context.DeleteAddress(id);

                if (deletedAddress != null)
                {
                    LogInformationRequestResponse(Ok());
                    return Ok(deletedAddress);
                }
                else
                {
                    LogWarningRequestResponse(NotFound(), "Address not found in database");
                    return NotFound();
                }
            }
            else
            {
                string message = "The id attribute cannot be null or whitespace";
                ModelState.AddModelError("", message);

                LogWarningRequestResponse(BadRequest(), message);
                return BadRequest(ModelState);
            }
        }

        protected void LogRequestPath()
        {
            _logger.LogInformation("Request path: " + HttpContext.Request.Path);
        }

        protected void LogInformationRequestResponse(StatusCodeResult result)
        {
            _logger.LogInformation("Status code: " + result.StatusCode.ToString());
        }

        protected void LogWarningRequestResponse(StatusCodeResult result, string message)
        {
            _logger.LogWarning("Status code: " + result.StatusCode.ToString() + "\n\t" + message);
        }
    }
}