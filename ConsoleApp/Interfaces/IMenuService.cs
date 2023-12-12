namespace ConsoleApp.Interfaces
{
    // Need to rework menuservice and imenuservice and move to this interface
    // and then implement the service. then clear out stuff from the menuservice
    // and maybe move some logic elsewhere? 
    public interface IMenuService
    {
        void ShowMenu();
        void ListContactByEmail(string email);
    }
}