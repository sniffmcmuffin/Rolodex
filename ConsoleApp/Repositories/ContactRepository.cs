using ConsoleApp.Interfaces;
using ConsoleApp.Models.Responses;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace ConsoleApp.Repositories;

public class ContactRepository : IContactRepository
{
    private List<IContact> _contactList;
    private IFileService? _fileService;
    private string? email;

    public ContactRepository(IFileService fileService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _contactList = _fileService.LoadContactsFromFile();
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

          //   _contactList = _fileService.LoadContactsFromFile();

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

    public IServiceResult GetContactByEmail(Func<Contact, bool> predicate)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (_fileService == null)
            {
                throw new InvalidOperationException("_fileService is null.");
            }

            _contactList = _fileService.LoadContactsFromFile();

            // Find the contact with the specified predicate
            var contact = _contactList.OfType<Contact>().FirstOrDefault(predicate);

            if (contact != null)
            {
                response.Status = Enums.ServiceStatus.SUCCESSED;
                response.Result = new List<IContact> { contact };
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
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

