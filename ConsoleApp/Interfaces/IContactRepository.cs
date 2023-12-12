using ConsoleApp.Models;

namespace ConsoleApp.Interfaces
{
    public interface IContactRepository
    {
        IServiceResult AddContact(IContact contact);
        IServiceResult DeleteContact(Func<IContact, bool> predicate);
        IServiceResult DeleteContactByEmail(string email);
        IServiceResult GetAllContacts();
        //  IServiceResult GetContactByEmail(string email);
        IServiceResult GetContactByEmail(Func<Contact, bool> predicate);
        IServiceResult UpdateContact(IContact contact);
    }
}