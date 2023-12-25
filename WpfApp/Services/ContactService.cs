using Shared.Interfaces;
using Shared.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using Shared.Models.Responses;
using Shared.Repositories;
using Newtonsoft.Json;
using Shared.Services;
using Shared.Enums;

namespace WpfApp.Services;

public class ContactService
{
    private List<Contact> _contacts = [];
    //  private List<Contact> _contacts = new List<Contact>();
    private readonly FileService _fileService;
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository, string filePath)
    {
        // _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        // _fileService = new FileService(filePath);

        _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        _fileService = new FileService(filePath);    }

    public void Add(Contact contact)
    {
        var result = _contactRepository.AddContact(contact);
        //  _contactService!.AddContact(contact);
        //  _contacts.Add(contact);
    }

    public IEnumerable<Contact> GetAll()
    {
       return _contacts;     
    }
    
    public IServiceResult GetAllContacts()
    {        
        return _contactRepository.GetAllContacts();
    }

    public IServiceResult GetContactFromList(Func<Contact, bool> predicate)
    {
        return _contactRepository.GetContactByEmail(predicate);
    }

    public Contact GetOne(Func<Contact, bool> predicate)
    {
        var contact = _contacts.FirstOrDefault(predicate);
        return contact ?? null!;
    }

    public Contact Update(Contact contact)
    {
        var existingContact = GetOne(x => x.Id == contact.Id);
        if (existingContact != null)
        {
            existingContact.Id = contact.Id;
            existingContact.firstName = contact.firstName;
            existingContact.lastName = contact.lastName;
            existingContact.email = contact.email;
            existingContact.phoneNumber = contact.phoneNumber;

            return existingContact;
        }
        return null!;
    }

    public void Remove(Contact contact)
    {
      var con = _contacts.FirstOrDefault(x => x.Id == contact.Id);
      if (con != null)
        {
            _contacts.Remove(con);
        }
    }

    public bool Exists(Func<Contact, bool> predicate)
    {
        return _contacts.Any(predicate);
    }
}