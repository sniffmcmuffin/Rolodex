using ConsoleApp.Interfaces;
using ConsoleApp.Models.Responses;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Repositories;

public class ContactRepository : IContactRepository
{
    private List<IContact> _contactList;
    private IFileService? _fileService;

    public ContactRepository(IFileService fileService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _contactList = LoadContactsFromStorage();
    }

    public IServiceResult AddContact(IContact contact)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (_fileService == null)
            {
                throw new InvalidOperationException("_fileService is null.");
            }

            // Lambda. X short/instead of contact
            if (!_contactList.Any(x => x.email == contact.email))
            {
                contact.Id = _contactList.Count + 1;
                _contactList.Add(contact);

                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contactList));
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


    public IServiceResult GetAllContacts()
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (_fileService == null)
            {
                throw new InvalidOperationException("_fileService is null.");
            }

            var content = _fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _contactList = JsonConvert.DeserializeObject<List<IContact>>(content)!;
            }

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

    private List<IContact> LoadContactsFromStorage()
    {
        List<IContact> contactList = new List<IContact>();

        try
        {
            string content = _fileService.GetContentFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                // Deserialize the JSON content into a list of Contacts
                List<Contact> deserializedList = JsonConvert.DeserializeObject<List<Contact>>(content);

                // Convert the list of Contacts to a list of IContact
                contactList = deserializedList.Cast<IContact>().ToList();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return contactList;
    }


}

