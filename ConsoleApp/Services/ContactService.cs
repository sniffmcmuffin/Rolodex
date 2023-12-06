using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using System.Diagnostics;

namespace ConsoleApp.Services;

public class ContactService : IContactService
{
    private readonly List<IContact> _contactList = [];

    public bool AddToList(IContact contact)
    {
        //  throw new NotImplementedException(); // Gives not implemented error when testing.
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

    public IEnumerable<IContact> GetAllFromList()
    {
        try
        {
            return _contactList;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!; // Since its empty, not true/false.
    }
}