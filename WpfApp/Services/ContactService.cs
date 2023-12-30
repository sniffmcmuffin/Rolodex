using Shared.Interfaces;
using Shared.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using Shared.Models.Responses;
using Shared.Repositories;
using Newtonsoft.Json;
using Shared.Services;
using Shared.Enums;
using System.Diagnostics;
using System.Windows;
using System;
using System.Linq;
using System.Collections.ObjectModel;

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
        _fileService = new FileService(filePath);  
    }

        public IServiceResult Add(IContact contact)
        {

        Debug.WriteLine("Contacts in List: " + string.Join(", ", _contacts.Select(c => c.Id)));

        Debug.WriteLine("Now in Add function in ContactService. Contact: " + JsonConvert.SerializeObject(contact));
        var result = _contactRepository.AddContact(contact);

        switch (result.Status)
        {
            case Shared.Enums.ServiceStatus.SUCCESSED:
                Debug.WriteLine("Added new contact successfully.");
                break;
            case Shared.Enums.ServiceStatus.ALREADY_EXISTS:
                Debug.WriteLine("Contact already exists.");
                break;
            case Shared.Enums.ServiceStatus.FAILED:
                Debug.WriteLine("Failed to add new contact.");
                Debug.WriteLine("Guru Meditation: " + result.Result.ToString());
                break;
        }

        return result;
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
        Debug.WriteLine("Searching for contact with predicate: " + predicate);

        var contact = _contacts.FirstOrDefault(predicate);

        if (contact != null)
        {
            Debug.WriteLine("Contact found: " + contact);
        }
        else
        {
            Debug.WriteLine("Contact not found with the given predicate.");
        }

        return contact ?? null!;
    }

    public Contact Update(Contact contact)
    {
        Debug.WriteLine("UPDATE " + contact);

        Debug.WriteLine($"Existing Contact IDs: {string.Join(", ", _contacts.Select(c => c.Id))}");

        Debug.WriteLine("Contact ID: " + contact.Id);

        var existingContact = _contacts.FirstOrDefault(x => x.Id == contact.Id);
        Debug.WriteLine($"Existing Contact IDs: {_contacts.Select(c => c.Id)}");

        if (existingContact != null)
        {
            Debug.WriteLine("EXISTING CONTACT " + existingContact);
            Debug.WriteLine("EXISTING CONTACT ID " + existingContact.Id);

            Debug.WriteLine("UPDATE 1 " + contact.firstName);
           
            existingContact.firstName = contact.firstName;
            existingContact.lastName = contact.lastName;
            existingContact.email = contact.email;
            existingContact.phoneNumber = contact.phoneNumber;

            Debug.WriteLine("UPDATE 2 " + existingContact);
            return existingContact;
        }

        Debug.WriteLine("Existing contact not found.");

        // If contact not found, add it to the list
        _contacts.Add(contact);

        Debug.WriteLine("Contact added to the list: " + contact);

        return contact;
    }

    public IServiceResult Remove(Func<IContact, bool> predicate)
    {
        return _contactRepository.DeleteContact(predicate);
    }

    public bool Exists(Func<Contact, bool> predicate)
    {
        return _contacts.Any(predicate);
    }
}