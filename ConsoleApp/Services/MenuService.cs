using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using System.Runtime.CompilerServices;

namespace ConsoleApp.Services;


public interface IMenuService
{
    // Possible menu styling
    // Meny:
    // 1) Kontakter
    //    - Lägg till
    //    - Ändra
    //    - Radera
    // 2) Lista
    //    - Lista en användare
    //    - Lista alla användare
    // 3) Inställningar
    //    - Färgschema
    //    - snabbt/långsamt (om jag har tid)
    // 4) Avbryt
    void ShowMenu();
}
public class MenuService : IMenuService 
{
    private readonly IContactService _contactService = new ContactService();
    public void ShowMenu()
    {
        while (true)
        {
            MenuHeader("MENU OPTIONS");
            Console.WriteLine($"{"1.", -3} Contacts");
            Console.WriteLine($"{"2.", -3} Lists");
            Console.WriteLine($"{"3.", -3} Settings");
            Console.WriteLine($"{"4.", -3} Quit");
            Console.WriteLine("Enter menu option: ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    ContactMenu(); 
                    break;
                case "2":
                    ListMenu();
                    break;
                case "3":
                    SettingsMenu();
                    break;
                case "4":
                    QuitApp();
                    break;
                default:
                    DisplayPressAnyKey();
                    break;
            }
           // Console.ReadKey();
        }
    }

    private void ContactMenu()
    {
        // Add new contact
        IContact contact = new Contact();
        MenuHeader("Add new contact");
        Console.Write("First name: ");
        contact.firstName = Console.ReadLine() ?? "";
        Console.Write("Last name: ");
        contact.lastName = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        contact.email = Console.ReadLine() ?? "";
        Console.Write("Phone number: ");
        contact.phoneNumber = Console.ReadLine() ?? "";

        var res = _contactService.AddContact(contact);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("Added new contact successfully."); // Todo: Make it like added new contact, nameofcontact, successfully.
                break;
            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("Contact already exists.");
                break;
            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed to add new contact.");
                Console.WriteLine("Guru Meditation: " + res.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }

    private void ListMenu()
    {
        MenuHeader("Contact List");
        var res = _contactService.GetAllContacts();

        if (res.Status == Enums.ServiceStatus.SUCCESSED)
        {           
            if (res.Result is List<IContact> contactList)
            {
                if (!contactList.Any())
                {
                    Console.WriteLine("No contacts found.");
                }
                else
                {
                    foreach (var contact in contactList)
                    {
                        Console.WriteLine($"{contact.firstName} {contact.lastName} <{contact.email}>");
                    }
                }
                
            }
        }
        DisplayPressAnyKey();
    }

    private void AddContactMenu()
    {
      
    }

    private void DeleteContactMenu()
    {
        
    }

    private void SettingsMenu()
    {
        throw new NotImplementedException();
    }

    private void UpdateContactMenu()
    {
        throw new NotImplementedException();
    }

    private void ViewContactMenu()
    {
        throw new NotImplementedException();
    }
    
    private void MenuHeader(string header) // Base menu.
    {
        Console.Clear();
        Console.WriteLine($"Rolodex {header} Menu");
        Console.WriteLine();
    }

    private void QuitApp() // Make a nifty exit
    {
        // Console.Clear(); 
        Console.Write("Quit application? [y/n]");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            Environment.Exit(0);
    }

    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}