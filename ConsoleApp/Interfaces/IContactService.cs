
namespace ConsoleApp.Interfaces;

public interface IContactService
{
    bool AddToList(IContact contact);
    IEnumerable<IContact> GetAllFromList();
}
