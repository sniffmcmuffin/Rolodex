namespace ConsoleApp.Interfaces
{
    public interface IContactRepository
    {
        IServiceResult AddContact(IContact contact);
        IServiceResult DeleteContact(Func<IContact, bool> predicate);
        IServiceResult DeleteContactByEmail(string email);
        IServiceResult GetAllContacts();
        IServiceResult GetContactByEmail(string email);
        IServiceResult UpdateContact(IContact contact);
    }
}