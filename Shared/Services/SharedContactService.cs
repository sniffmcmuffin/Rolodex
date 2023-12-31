﻿using Shared.Interfaces;
using Shared.Models;
using Shared.Models.Responses;
using Shared.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Shared.Services;

public class SharedContactService : ISharedContactService
{
    private readonly FileService _fileService;
    private readonly IContactRepository _contactRepository;

    public SharedContactService(IContactRepository contactRepository, string filePath)
    {
        // _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        // _fileService = new FileService(filePath);

        _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        _fileService = new FileService(filePath);

    }

    public IServiceResult AddContact(IContact contact)
    {
        var result = _contactRepository.AddContact(contact);

        switch (result.Status)
        {
            case Shared.Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("Added new contact successfully.");
                break;
            case Shared.Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("Contact already exists.");
                break;
            case Shared.Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed to add new contact.");
                Console.WriteLine("Guru Meditation: " + result.Result.ToString());
                break;
        }

        return result;
    }

    public IServiceResult GetAllContacts()
    {
        return _contactRepository.GetAllContacts();
    }

    public IServiceResult GetContactFromList(Func<Contact, bool> predicate)
    {
        var response = _contactRepository.GetContactByEmail(predicate);

        if (response.Status == Enums.ServiceStatus.NOT_FOUND)
        {
            response.Result = new List<IContact>(); // Set an empty list for NOT_FOUND
        }

        return response;
    }

    public IServiceResult DeleteContact(Func<IContact, bool> predicate)
    {
        return _contactRepository.DeleteContact(predicate);
    }
}