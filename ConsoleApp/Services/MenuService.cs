using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Repositories;
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
    private readonly IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void ShowMenu()
    {
        while (true)
        {   // I could have done something like 0-9 options, but I wanted submenus. 
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
        }
    }

    private void ContactMenu()
    {
        MenuHeader("CONTACT MENU");
        Console.WriteLine($"{"1.",-3} Add new contact");
        Console.WriteLine($"{"2.",-3} Update a contact");
        Console.WriteLine($"{"3.",-3} Delete a contact");
        Console.WriteLine($"{"4.",-3} Delete all contacts");
        Console.WriteLine($"{"5.",-3} Return to main menu");
        Console.WriteLine("Enter menu option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": // Add new contact.
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

                _contactService!.AddContact(contact);

                DisplayPressAnyKey(); ContactMenu();
                break;
            case "2":
                ListMenu();
                break;
            case "3":
                SettingsMenu();
                break;
            case "4":
                SettingsMenu();
                break;
            case "5": // Returning to main menu.
                ShowMenu();
                break;
            default:
                DisplayPressAnyKey();
                break;
        }
        
    }

    private void ListMenu()
    {
        MenuHeader("Contact List");

        var result = _contactService!.GetAllContacts();

        if (result.Status == Enums.ServiceStatus.SUCCESSED)
        {
            if (result.Result is List<IContact> contactList)
            {
                if (!contactList.Any())
                {
                    Console.WriteLine("No contacts found.");
                }
                else
                {
                    Console.WriteLine($"{PadRight("First Name", 15)} {PadRight("Last Name", 15)} {PadRight("Email", 30)}");

                    foreach (var contact in contactList)
                    {
                        Console.WriteLine($"{PadRight(contact.firstName, 15)} {PadRight(contact.lastName, 15)} {PadRight(contact.email, 30)}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Failed to retrieve contacts: {result.Result}");
        }

        DisplayPressAnyKey();
    }

    private string PadRight(string input, int length)
    {
        return input.PadRight(length);
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