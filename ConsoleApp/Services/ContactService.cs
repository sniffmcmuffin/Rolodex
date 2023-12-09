using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp.Services;

public class ContactService : IContactService
{
    private static readonly List<IContact> _contactList = [];

    public bool AddToList(IContact contact)
    {
        // Gult test = refactoring
        try
        {
            contact.Id = _contactList.Count + 1;
            _contactList.Add(contact);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IServiceResult AddContact(IContact contact) // Merge AddContact with AddToList and keep testing and response.
    {
        IServiceResult response = new ServiceResult();

        try
        {   // Lambda. X short/instead of contact
            if (!_contactList.Any(x => x.email == contact.email))
            {
                _contactList.Add(contact);
                response.Status = Enums.ServiceStatus.SUCCESSED;
            }
            else 
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }                
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    public IServiceResult DeleteContact(Func<Contact, bool> predicate)
    {
        //  IContact contact = _contactList.FirstOrDefault(predicate);
        throw new NotImplementedException();
    }

    public IServiceResult GetAllContacts()
    {
        IServiceResult response = new ServiceResult();

        try
        {
            response.Status = Enums.ServiceStatus.SUCCESSED;
            response.Result = _contactList;
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    public IEnumerable<IContact> GetAllFromList()
    {
        try
        {
            return _contactList;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!; // Since its empty, not true/false.
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

// Contact Management