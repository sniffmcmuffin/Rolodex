using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using ConsoleApp.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class ContactService : IContactService
{
    private List<IContact> _contactList = [];
    private FileService _fileService = new FileService(@"../../../contacts.json");

    private readonly ContactRepository _contactRepository;

    public ContactService(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }
       
    public IServiceResult AddContact(IContact contact)
    {
      return _contactRepository.AddContact(contact);
    }

    public IServiceResult DeleteContact(Func<Contact, bool> predicate)
    {      
        throw new NotImplementedException();
    }

    public IServiceResult GetAllContacts()
    {
        return _contactRepository.GetAllContacts();
    }

    public IEnumerable<IContact> GetAllFromList() // IEnumerable är läsbar lista.
    {
        try
        {
            var content = _fileService.GetContentFromFile();
            {
                if (!string.IsNullOrEmpty(content))
                {
                    _contactList = JsonConvert.DeserializeObject<List<IContact>>(content)!;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return _contactList;
    }

    public IServiceResult GetContactFromList(Func<Contact, bool> predicate)
    {
        throw new NotImplementedException();
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