using KonsolApp.Interfaces;
using Shared.Interfaces;
using Shared.Models;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace KonsolApp.Services;

public class MenuService : IMenuService
{
    private readonly ISharedContactService _contactService;
    string UpdateOrDelete = null!;

    public MenuService(ISharedContactService contactService)
    {
        _contactService = contactService;
    }

    public void ShowMenu()
    {
        while (true)
        {   // I could have done something like 0-9 options, but I wanted submenus. 
            MenuHeader("MAIN");
            Console.WriteLine($"| {"1.",-3} Contacts");
            Console.WriteLine($"| {"2.",-3} Lists");
        //    Console.WriteLine($"| {"3.",-3} Settings"); For the future.
            Console.WriteLine($"| {"3.",-3} Quit");
            Console.WriteLine("| Enter menu option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ContactMenu();
                    break;
                case "2":
                    ListMenu();
                    break;
                //case "3":
                //    SettingsMenu();
                //    break;
                case "3":
                    QuitApp();
                    break;
                default:
                    Console.WriteLine("No such option.");
                    DisplayPressAnyKey();
                    break;
            }
        }
    }

    private void ContactMenu()
    {
        MenuHeader("CONTACT");
        Console.WriteLine($"| {"1.",-3} Add new contact");      // Done
        Console.WriteLine($"| {"2.",-3} Update a contact");     // Done
        Console.WriteLine($"| {"3.",-3} Delete a contact");     // Done
        Console.WriteLine($"| {"4.",-3} Return to main menu");  // Done
        Console.WriteLine("| Enter menu option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": // Add new contact.
                MenuHeader("Add new contact");
                TestAdd();
                break;
            case "2":
                // This is probably lazy, but the idea for now:
                // Find contact by email and remove it. Then add new contact.
                // Update: Inser att man borde kunna välja att ha kvar vissa delar, men ändra andra.
                MenuHeader("Update a contact");
                Console.WriteLine("Enter email belonging to contact you wish update:");
                UpdateOrDelete = "updated";
                DeleteContact();
                TestAdd();
                DisplayPressAnyKey(); ContactMenu();
                break;
            case "3":
                MenuHeader("Delete a contact");
                Console.WriteLine("Enter email belonging to contact you wish to delete:");
                DeleteContact();
                UpdateOrDelete = "deleted";
                break;
            case "4": // Returning to main menu.
                ShowMenu();
                break;
            default:
                DisplayPressAnyKey();
                break;
        }
    }

    private void TestAdd()
    {
        IContact contact = new Contact();
        Console.Write("First name: ");
        contact.firstName = Console.ReadLine() ?? "";
        Console.Write("Last name: ");
        contact.lastName = Console.ReadLine() ?? "";
        Console.Write("Street: ");
        contact.street = Console.ReadLine() ?? "";
        Console.Write("Zipcode: ");
        contact.zipCode = Console.ReadLine() ?? "";
        Console.Write("City: ");
        contact.city = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        contact.email = Console.ReadLine() ?? "";
        Console.Write("Phone number: ");
        contact.phoneNumber = Console.ReadLine() ?? "";

        _contactService!.AddContact(contact);
        DisplayPressAnyKey(); ContactMenu();
    }

    private void ListMenu()
    {
        MenuHeader("LIST");
        Console.WriteLine($"| {"1.",-3} List a contact");         // Done
        Console.WriteLine($"| {"2.",-3} List all contacts");      // Done
        Console.WriteLine($"| {"3.",-3} Return to main menu");    // Done
        Console.WriteLine("| Enter menu option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": // List a contact by email.
                Console.WriteLine("Enter email to search for a contact:");
                ShowContactByEmail();
                break;
            case "2": // List all contacts. Lets move this out of the switch later on.
                var result = _contactService!.GetAllContacts();

                if (result.Status == Shared.Enums.ServiceStatus.SUCCESSED)
                {
                    if (result.Result is List<IContact> contactList)
                    {
                        if (!contactList.Any())
                        {
                            Console.WriteLine("No contacts found.");
                        }
                        else
                        {
                            Console.WriteLine("+------------------------------------------------------------------------");
                            Console.WriteLine($" | {PadRight("First Name", 15)} {PadRight("Last Name", 15)} {PadRight("Email", 30)}");

                            foreach (var contact in contactList)
                            {
                                Console.WriteLine($" | {PadRight(contact.firstName, 15)} {PadRight(contact.lastName, 15)} {PadRight(contact.email, 30)}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve contacts: {result.Result}");
                }

                DisplayPressAnyKey(); ListMenu();
                break;
            case "3": // Returning to main menu.
                ShowMenu();
                break;
            default:
                DisplayPressAnyKey();
                break;
        }
    }

    private string PadRight(string input, int length)
    {
        return (input ?? "").PadRight(length);
    }

    private void MenuHeader(string header) // Base menu.
    {
        Console.Clear();
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        Console.WriteLine("=                             R O L O D E X                             =");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        Console.WriteLine($"| [{header}]");
        Console.WriteLine("|");
    }

    private void QuitApp() // Make a neat exit
    {
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

    public void ListContactByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public void ShowContactByEmail()
    {
        string email = Console.ReadLine() ?? "default@email.com";

        Func<Contact, bool> predicate = contact => contact.email.Equals(email, StringComparison.OrdinalIgnoreCase);

        var result = _contactService.GetContactFromList(predicate);

        if (result.Status == Shared.Enums.ServiceStatus.SUCCESSED)
        {
            var contacts = result.Result as List<IContact>;

            if (contacts != null)
            {
                  Console.WriteLine("+------------------------------------------------------------------------");
                  MenuHeader("Kontakt");
                //Console.WriteLine($" | {PadRight("Name", 30)} {PadRight("Email", 30)}");
                //Console.WriteLine($" | {PadRight("Street", 30)} {PadRight("Phonenumber", 30)} ");
                //Console.WriteLine($" | {PadRight("Adress", 30)} ");

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"| {PadRight($"{contact.firstName} {contact.lastName}", 30)} {PadRight(contact.email, 30)}");
                    Console.WriteLine($"| {PadRight(contact.street, 30)} {PadRight(contact.phoneNumber, 30)} ");
                    Console.WriteLine($"| {PadRight($"{contact.zipCode}, {contact.city}", 30)} ");
                }
            }
            else
            {
                Console.WriteLine("Result is not of expected type.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to retrieve contact: {result.Result}");
        }

        DisplayPressAnyKey(); ListMenu();
    }

    public void DeleteContact()
    {
        string email = Console.ReadLine() ?? "default@email.com";

        Func<Contact, bool> predicate = contact => contact.email.Equals(email, StringComparison.OrdinalIgnoreCase);

        var result = _contactService.GetContactFromList(predicate);

        if (result.Status == Shared.Enums.ServiceStatus.SUCCESSED)
        {
            var contacts = result.Result as List<IContact>;

            if (contacts != null && contacts.Any())
            {
                // Assuming we want to delete the first contact found with the specified email.
                var contactToDelete = contacts.First();
                var deleteResult = _contactService.DeleteContact(c => c.Id == contactToDelete.Id);

                if (deleteResult.Status == Shared.Enums.ServiceStatus.SUCCESSED)
                {
                    Console.WriteLine($"Contact being {UpdateOrDelete} updated..."); // Not perfect.
                }
                else
                {
                    Console.WriteLine($"Failed to delete contact: {deleteResult.Result}");
                }
            }
            else
            {
                Console.WriteLine("No contacts found with the specified email.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to retrieve contact: {result.Result}");
        }
    }
}     