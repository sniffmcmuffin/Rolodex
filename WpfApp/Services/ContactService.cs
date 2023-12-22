using Shared.Models;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace WpfApp.Services;

public class ContactService
{
    private List<Contact> _contacts = [];
    
    public bool Add(Contact contact)
    {
        if (contact == null)
            return false;

        _contacts.Add(contact);
         return true;
    }

    public IEnumerable<Contact> GetAll()
    {
        return _contacts;
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

    public bool Remove(Contact contact)
    {
        if (!_contacts.Contains(contact))
            return false;

        _contacts.Remove(contact);
        return true;
    }

    public bool Exists(Func<Contact, bool> predicate)
    {
        return _contacts.Any(predicate);
    }
}
