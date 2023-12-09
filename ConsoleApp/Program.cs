using ConsoleApp.Interfaces;
using ConsoleApp.Models;
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
    }
}
