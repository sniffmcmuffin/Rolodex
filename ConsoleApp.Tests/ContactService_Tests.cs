using Shared.Enums;  // Add this using directive

using Shared.Interfaces;
using Shared.Models;
using Shared.Models.Responses;
using Shared.Services;
using Moq;
using Xunit;

namespace ConsoleApp.Tests
{
    public class ContactService_Tests
    {
        [Fact]
        public void AddToListShouldAddOneContactToContactList_ThenReturnTrue()
        {
            // Arrange
            var mockRepository = new Mock<IContactRepository>();
            //   var contactService = new ContactService(mockRepository.Object, @"../../../../Shared/contacts.json");
            var contactService = new SharedContactService(mockRepository.Object, @"../../../../Shared/contacts.json");


            IContact contact = new Contact { firstName = "Jimmy" };

            // Setup mock
            mockRepository.Setup(r => r.AddContact(It.IsAny<IContact>()))
                          .Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED });

            // Act
            var result = contactService.AddContact(contact);

            // Assert
            Assert.Equal(ServiceStatus.SUCCESSED, result.Status);
        }

        [Fact]
        public void GetAllFromListShould_GetAllContactsInContactsList_ThenReturnListOfContact()
        {
            // Arrange
            var mockRepository = new Mock<IContactRepository>();
            var contactService = new SharedContactService(mockRepository.Object, @"../../../contacts.json");

            IContact contact = new Contact { firstName = "Jimmy",
                                             lastName = "Sjöström",
                                             email = "jimmy.sjostrom@domain.se", 
                                             phoneNumber = "075-343564454",
                                             city = "Skyffladyngamåla",
                                             zipCode = "434 43", 
                                             street = "Rallarsvängen 7",
                                             CompanyName = "C#",
                                             ContactPerson = "Kermit"};

            mockRepository.Setup(r => r.GetAllContacts()).Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED, Result = new List<IContact> { contact } });

            // Act
            var result = contactService.GetAllContacts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ServiceStatus.SUCCESSED, result.Status);

            var contactList = result.Result as List<IContact>;
            Assert.NotNull(contactList);
            Assert.Equal(1, contactList.Count);

            IContact returnedContact = contactList.FirstOrDefault()!;
            Assert.NotNull(returnedContact);
            Assert.Equal("Jimmy", returnedContact.firstName);
            Assert.Equal("Sjöström", returnedContact.lastName);
        }

        [Fact]
        public void GetContactFromListShouldReturnNotFound_WhenPredicateDoesNotMatch()
        {
            // Arrange
            var mockRepository = new Mock<IContactRepository>();
            var contactService = new SharedContactService(mockRepository.Object, @"../../../../Shared/contacts.json");

            mockRepository.Setup(r => r.GetContactByEmail(It.IsAny<Func<Contact, bool>>()))
                          .Returns(new ServiceResult { Status = ServiceStatus.NOT_FOUND });

            // Act
            var result = contactService.GetContactFromList(contact => contact.email == "nonexistent@example.com");

            // Assert
            Assert.Empty(result.Result as List<IContact>);
        }

        [Fact]
        public void DeleteContactByEmailShouldRemoveContactFromTheList_ThenReturnTrue()
        {
            // Arrange
            var mockRepository = new Mock<IContactRepository>();
            var contactService = new SharedContactService(mockRepository.Object, @"../../../contacts.json");

            // Setup mock for deleting contact by email
            mockRepository.Setup(r => r.DeleteContact(It.IsAny<Func<IContact, bool>>()))
                          .Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED });

            // Act
            var deleteResult = contactService.DeleteContact(c => c.email == "jimmy@example.com");

            // Assert
            Assert.Equal(ServiceStatus.SUCCESSED, deleteResult.Status);
        }

    }
}
