
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;

namespace ConsoleApp.Interfaces;

public interface IContactService
{
   // bool AddToList(IContact contact); // Testing purposes
    IEnumerable<IContact> GetAllFromList();
    IServiceResult AddContact(IContact contact); 
    IServiceResult GetContactFromList(Func<Contact, bool> predicate);
    IServiceResult GetContactByEmail(string email);
    IServiceResult GetAllContacts();
    IServiceResult UpdateContact(IContact contact);
    IServiceResult DeleteContact(Func<IContact, bool> predicate);
    IServiceResult DeleteContactByEmail(string email);

}
