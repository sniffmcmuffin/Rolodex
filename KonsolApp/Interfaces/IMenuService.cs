namespace KonsolApp.Interfaces
{
    public interface IMenuService
    {
        void DeleteContact();
        void ListContactByEmail(string email);
        void ShowContactByEmail();
        void ShowMenu();
    }
}