﻿using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Xunit;

namespace ConsoleApp.Tests;


public class ContactService_Tests
{
    [Fact]
    public void AddToListShouldAddOneContactToContactList_ThenReturnTrue()
    {
        // Arrange
        IContact contact = new Contact { firstName = "Jimmy" };
        IContactService contactService = new ContactService();

        // Act
        var result = contactService.AddToList(contact);

        // Assert
        Assert.True(result);
    }

    [Fact] // Test each property in IContact.
    public void GetAllFromListShould_GetAllContactsInContactsList_ThenReturnListOfContact()
    {
        // Arrange
        IContactService contactService = new ContactService();
        IContact contact = new Contact { firstName = "Jimmy", lastName = "Sjöström" };
        contactService.AddToList(contact);

        // Act
        IEnumerable<IContact> result = contactService.GetAllFromList();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());      
        IContact returnedContact = result.FirstOrDefault()!;
        Assert.NotNull(returnedContact);
        Assert.Equal(1, returnedContact.Id);
    }
}
