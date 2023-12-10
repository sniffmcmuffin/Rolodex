using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Repositories;
using ConsoleApp.Services;

class Program
{
  static void Main()
  {
        Console.WindowWidth = 200; // For the logo

        // Loading at startup
        IContact contact = new Contact();
        var IMenuService = new MenuService();
        IMenuService.ShowMenu();
        // var contentFile = new FileService(@"contacts.txt");
       
       // var contactRepository = new ContactRepository(new List<IContact>());
       // var contactService = new ContactService(contactRepository, new FileService(@"../../../contacts.json"));

    }
}
