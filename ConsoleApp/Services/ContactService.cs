using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class ContactService : IContactService
{
    private List<IContact> _contactList = [];
    private FileService _fileService = new FileService(@"../../../contacts.json");

    // Gult test = refactoring
    public IServiceResult AddContact(IContact contact)
    {
        IServiceResult response = new ServiceResult();
        bool TestSuccess = false; // True or false for testing purposes

        try
        {
            // Lambda. X short/instead of contact
            if (!_contactList.Any(x => x.email == contact.email))
            {
                contact.Id = _contactList.Count + 1;
                _contactList.Add(contact);               

                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contactList));
                TestSuccess = true; 
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
            TestSuccess = false; 
        }
                
        response.Status = TestSuccess ? Enums.ServiceStatus.SUCCESSED : Enums.ServiceStatus.FAILED;

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

// Contact Management