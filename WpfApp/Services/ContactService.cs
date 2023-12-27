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

    //public void Add(Contact contact)
    //{
    //    Debug.WriteLine("Now in Add function in ContactService. Contact: " + JsonConvert.SerializeObject(contact));

    //    Application.Current.Dispatcher.Invoke(() =>
    //    {
    //        var result = _contactRepository.AddContact(contact);

    //        //  _contactService!.AddContact(contact);
    //        //  _contacts.Add(contact);

    //    });


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

    //public IServiceResult UpdateContact(IContact updatedContact)
    //{
    //    IServiceResult response = new ServiceResult();


    //    try
    //    {
    //        if (_fileService == null)
    //        {
    //            throw new InvalidOperationException("_fileService is null.");
    //        }

    //        _contactList = _fileService.LoadContactsFromFile();

    //        var existingContact = _contactList.OfType<Contact>().FirstOrDefault(x => x.email == updatedContact.email);

    //        if (existingContact != null)
    //        {
    //            // Update the existing contact
    //            existingContact.firstName = updatedContact.firstName;
    //            existingContact.lastName = updatedContact.lastName;
    //            existingContact.street = updatedContact.street;
    //            // ... update other properties

    //            // Save the updated list to file
    //            _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contactList));

    //            response.Status = Shared.Enums.ServiceStatus.SUCCESSED;
    //        }
    //        else
    //        {
    //            response.Status = Shared.Enums.ServiceStatus.NOT_FOUND;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //        response.Status = Shared.Enums.ServiceStatus.FAILED;
    //        response.Result = ex.Message;
    //    }

    //    return response;
    //}

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