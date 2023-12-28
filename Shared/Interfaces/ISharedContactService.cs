using Shared.Models;

namespace Shared.Interfaces
{
    public interface ISharedContactService
    {
        IServiceResult AddContact(IContact contact);
        IServiceResult DeleteContact(Func<IContact, bool> predicate);
        IServiceResult DeleteContactByEmail(string email);
        IServiceResult GetAllContacts();
        IServiceResult GetContactByEmail(string email);
        IServiceResult GetContactFromList(Func<Contact, bool> predicate);
        IServiceResult UpdateContact(IContact contact);
    }
}