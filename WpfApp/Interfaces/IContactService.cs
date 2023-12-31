using Shared.Interfaces;
using Shared.Models;

namespace WpfApp.Interfaces
{
    public interface IContactService
    {
        IServiceResult Add(IContact contact);
        bool Exists(Func<Contact, bool> predicate);
        IEnumerable<Contact> GetAll();
        IServiceResult GetAllContacts();
        IServiceResult GetContactFromList(Func<Contact, bool> predicate);
        Contact GetOne(Func<Contact, bool> predicate);
        IServiceResult Remove(Func<IContact, bool> predicate);
        Contact Update(Contact contact);
    }
}