using ConsoleApp.Enums;
using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using ConsoleApp.Services;
using Moq;
using Xunit;

namespace ConsoleApp.Tests;


public class ContactService_Tests
{
    [Fact]
    public void AddToListShouldAddOneContactToContactList_ThenReturnTrue()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        //  var contactService = new ContactService(mockRepository.Object);
        var contactService = new ContactService(mockRepository.Object, @"../../../contacts.json");

        IContact contact = new Contact { firstName = "Jimmy" };

        // Setup mock
        mockRepository.Setup(r => r.AddContact(It.IsAny<IContact>()))
                      .Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED });

        // Act
        var result = contactService.AddContact(contact);

        // Assert
        Assert.Equal(ServiceStatus.SUCCESSED, result.Status);
    }
      
    [Fact] // Fact is that this test blew up in my face multiple times. 
    public void GetAllFromListShould_GetAllContactsInContactsList_ThenReturnListOfContact()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        //  var contactService = new ContactService(mockRepository.Object);
        var contactService = new ContactService(mockRepository.Object, @"../../../contacts.json");

        IContact contact = new Contact { firstName = "Jimmy", lastName = "Sjöström", email = "jimmy.sjostrom@domain.se", phoneNumber ="075-343564454" };
               
        mockRepository.Setup(r => r.GetAllContacts()).Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED, Result = new List<IContact> { contact } });

        // Act
        var result = contactService.GetAllContacts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceStatus.SUCCESSED, result.Status);
              
        var contactList = result.Result as List<IContact>;
        Assert.NotNull(contactList);
        Assert.Equal(1, contactList.Count); // Gah
       // Assert.Single(1, contactList.Count); 
             
        IContact returnedContact = contactList.FirstOrDefault()!;
        Assert.NotNull(returnedContact);
        Assert.Equal("Jimmy", returnedContact.firstName);
        Assert.Equal("Sjöström", returnedContact.lastName);
    }

    [Fact]
    public void DeleteContactByEmailShouldRemoveContactFromTheList_ThenReturnTrue()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var contactService = new ContactService(mockRepository.Object, @"../../../contacts.json");

        // Setup mock for deleting contact by email
        mockRepository.Setup(r => r.DeleteContact(It.IsAny<Func<IContact, bool>>()))
                      .Returns(new ServiceResult { Status = ServiceStatus.SUCCESSED });

        // Act
        var deleteResult = contactService.DeleteContact(c => c.email == "jimmy@example.com");

        // Assert
        Assert.Equal(ServiceStatus.SUCCESSED, deleteResult.Status);
    }





}

