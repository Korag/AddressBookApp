using AddressBook_API.Controllers;
using AddressBook_API.DAL;
using AddressBook_API.DTO;
using AddressBook_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AddressBook_Tests
{
    public class AddressBookControllerTests
    {
        [Fact]
        public void GetAddressesByCityName_IfValidCityName_ReturnNotNullAddress()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetAddressesByCityName("Bielsko-Bia³a"))
                .Returns(new List<AddressModel>()
                                 {
                                     new AddressModel()
                                         {
                                             Id = Guid.NewGuid().ToString(),
                                             FirstName = "Jan",
                                             LastName = "Kowalski",
                                             City = "Bielsko-Bia³a",
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
                                             City = "Bielsko-Bia³a",
                                             Street = "Scenariusz",
                                             PostCode = "43-300",
                                             ApartmentNumber = "4/43",
                                             PhoneNumber = "987654321"
                                         }
                                 });

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetAddressesByCityName("Bielsko-Bia³a");
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAddressesByCityName_IfEmptyCityName_ReturnBadRequestStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetAddressesByCityName(""))
                .Returns(new List<AddressModel>());

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetAddressesByCityName("");
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void GetLastAddedAddress_IfDbIsEmpty_ReturnNullAddress()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetLastAddedAddress())
                .Returns((AddressModel)null);

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetLastAddedAddress();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
        }

        [Fact]
        public void GetLastAddedAddress_IfDbIsNotEmpty_ReturnNotNullAddress()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetAllAddresses())
                .Returns(new List<AddressModel>()
                                 {
                                     new AddressModel()
                                         {
                                             Id = Guid.NewGuid().ToString(),
                                             FirstName = "Jan",
                                             LastName = "Kowalski",
                                             City = "Bielsko-Bia³a",
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
                                             City = "Bielsko-Bia³a",
                                             Street = "Scenariusz",
                                             PostCode = "43-300",
                                             ApartmentNumber = "4/43",
                                             PhoneNumber = "987654321"
                                         }
                                 });

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetAddressBook();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAddressBook_IfDbIsNotEmpty_ReturnNotNullAddressesArray()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetLastAddedAddress())
                .Returns(new AddressModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Tadeusz",
                    LastName = "Nowak",
                    City = "Bielsko-Bia³a",
                    Street = "Scenariusz",
                    PostCode = "43-300",
                    ApartmentNumber = "4/43",
                    PhoneNumber = "987654321"
                });

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetLastAddedAddress();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAddressBook_IfDbIsEmpty_ReturnNullAddressesArray()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.GetLastAddedAddress())
                .Returns((AddressModel)null);

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.GetLastAddedAddress();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
        }

        [Fact]
        public void DeleteAddress_IfIdIsNull_ReturnBadRequestStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.DeleteAddress(null);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteAddress_IfAddressRelatedWithIdNotExists_ReturnNotFoundStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            string fakeId = Guid.NewGuid().ToString();

            mockDb.Setup(context => context.DeleteAddress(fakeId))
                .Returns((AddressModel)null);

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.DeleteAddress(fakeId);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteAddress_IfAddressExists_ReturnOkStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            string id = "922695cd-5502-4a48-a26a-586ed49f03b7";

            mockDb.Setup(context => context.DeleteAddress(id))
                 .Returns(new AddressModel()
                 {
                     Id = id,
                     FirstName = "Tadeusz",
                     LastName = "Nowak",
                     City = "Bielsko-Bia³a",
                     Street = "Scenariusz",
                     PostCode = "43-300",
                     ApartmentNumber = "4/43",
                     PhoneNumber = "987654321"
                 });

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var result = controller.DeleteAddress(id);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AddNewAddress_IfModelStateIsNotValid_ReturnBadRequest()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            AddNewAddressDTO addressToAdd = new AddNewAddressDTO
            {
                FirstName = "TEST"
            };

            controller.ModelState.AddModelError("key", "error message");
            var result = controller.AddNewAddress(addressToAdd);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddNewAddress_IfModelStateIsValid_ReturnOkStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            mockDb.Setup(context => context.AddAddress(new AddressModel()))
                   .Returns(new AddressModel()
                   {
                       Id = Guid.NewGuid().ToString(),
                       FirstName = "TEST",
                       LastName = "TEST",
                       City = "TEST",
                       Street = "TEST",
                       PostCode = "43-300",
                       ApartmentNumber = "1",
                       PhoneNumber = "123456789"
                   });

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            AddNewAddressDTO addressToAdd = new AddNewAddressDTO
            {
                FirstName = "TEST",
                LastName = "TEST",
                City = "TEST",
                Street = "TEST",
                PostCode = "43-300",
                ApartmentNumber = "1",
                PhoneNumber = "123456789"
            };

            controller.ModelState.Clear();
            var result = controller.AddNewAddress(addressToAdd);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void EditAddress_IfModelStateIsNotValid_ReturnBadRequest()
        {
            var mockDb = new Mock<IAddressBookDbContext>();
            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            EditAddressDTO addressToEdit = new EditAddressDTO
            {
                FirstName = "TEST"
            };

            controller.ModelState.AddModelError("key", "error message");
            var result = controller.EditAddress(addressToEdit);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void EditAddress_IfModelStateIsValidButAddressDoesNotExist_ReturnNotFoundStatusCode()
        {
            var mockDb = new Mock<IAddressBookDbContext>();

            AddressModel newAddress = new AddressModel
            {
                Id = "922695cd-5502-4a48-a26a-586ed49f03b7",
                FirstName = "TEST",
                LastName = "TEST",
                City = "TEST",
                Street = "TEST",
                PostCode = "43-300",
                ApartmentNumber = "1",
                PhoneNumber = "123456789"
            };

            mockDb.Setup(context => context.EditAddress(newAddress))
                   .Returns((AddressModel)null);

            var mockLogger = new Mock<ILogger<AddressBookController>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/testPath";
            var controller = new AddressBookController(mockLogger.Object, mockDb.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            EditAddressDTO addressToEdit = new EditAddressDTO
            {
                Id = "922695cd-5502-4a48-a26a-586ed49f03b7",
                FirstName = "TEST",
                LastName = "TEST",
                City = "TEST",
                Street = "TEST",
                PostCode = "43-300",
                ApartmentNumber = "1",
                PhoneNumber = "123456789"
            };

            var result = controller.EditAddress(addressToEdit);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}