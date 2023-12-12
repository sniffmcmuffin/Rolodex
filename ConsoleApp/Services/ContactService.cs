using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using ConsoleApp.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class ContactService : IContactService
{
    private FileService _fileService = new FileService(@"../../../contacts.json");
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public IServiceResult AddContact(IContact contact)
    {
        var result = _contactRepository.AddContact(contact);

        switch (result.Status)
        {
            case Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("Added new contact successfully.");
                break;
            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("Contact already exists.");
                break;
            case Enums.ServiceStatus.FAILED:
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
        return _contactRepository.GetContactByEmail(predicate);
    }

    public IServiceResult GetContactByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IServiceResult UpdateContact(IContact contact)
    {
        throw new NotImplementedException();
    }

    public IServiceResult DeleteContact(Func<IContact, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IServiceResult DeleteContactByEmail(string email)
    {
        throw new NotImplementedException();
    }
}