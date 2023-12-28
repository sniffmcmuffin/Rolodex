using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;
using Shared.Repositories;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using WpfApp.Mvvm.Views;
using WpfApp.Services;

namespace WpfApp.Mvvm.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContactService _contactService;
    private readonly IContactRepository _contactRepository;
    private readonly IFileService _fileService;

    private ObservableCollection<Shared.Models.Contact> _contactList = new ObservableCollection<Shared.Models.Contact>();

    public ObservableCollection<Shared.Models.Contact> ContactList
    {
        get => _contactList;
        set => SetProperty(ref _contactList, value);
    }

    private Shared.Models.Contact _contact = new Shared.Models.Contact();

    public Shared.Models.Contact Contact
    {
        get => _contact;
        set => SetProperty(ref _contact, value);
    }

    private Shared.Models.Contact? _selectedPerson;

    public Shared.Models.Contact? SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            if (_selectedPerson != value)
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
    }
    public ContactAddViewModel(IServiceProvider serviceProvider, ContactService contactService, IFileService fileService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _contactRepository = _serviceProvider.GetRequiredService<IContactRepository>();
        _fileService = new FileService("../../../../Shared/contacts.json");

        // Load ContactList from file.
        LoadContactList();

        // Initialize SelectedPerson with default values.
        // Took me bigger half of a day to figure out this is best way (?) to add new contact.
        SelectedPerson = new Shared.Models.Contact
        {
            Id = GetNextContactId(ContactList),
            firstName = "New",
            lastName = "Contact",
            street = "Default Street",
            zipCode = "Default Zip",
            city = "Default City",
            email = "Default Email",
            phoneNumber = "Default Phone"
        };
    }

    private void LoadContactList()
    {
        var contacts = _fileService.LoadContactsFromFile();
        ContactList = new ObservableCollection<Shared.Models.Contact>(contacts.Cast<Shared.Models.Contact>());
    }

    // This is a mess.
    // There is something in FileService that I cant fix for now.
    // It want to save as object instead of an array. So for now 
    // I am doing the writing to file here and as soon as I know
    // how I will seperate concerns.
    [RelayCommand]
    public void Add()
    {
        try
        {
            // Make sure contact has correct ID.
            if (SelectedPerson != null)
            {
                Debug.WriteLine("ADD CHECKING NAME " + SelectedPerson.firstName);

                // Update Contact properties based on SelectedPerson
                Contact.Id = SelectedPerson.Id;
                Contact.firstName = SelectedPerson.firstName;
                Contact.lastName = SelectedPerson.lastName;
                Contact.email = SelectedPerson.email;
                Contact.phoneNumber = SelectedPerson.phoneNumber;
                Contact.street = SelectedPerson.street;
                Contact.city = SelectedPerson.city;
                Contact.zipCode = SelectedPerson.zipCode;
            }

            Debug.WriteLine("In Add function. Contact: " + JsonConvert.SerializeObject(Contact));

            var existingContacts = _fileService.LoadContactsFromFile();

            // Update or add current contact
            if (existingContacts.Any(c => c.Id == Contact.Id))
            {
                var existingContact = existingContacts.First(c => c.Id == Contact.Id);
                existingContact.firstName = Contact.firstName;
                existingContact.lastName = Contact.lastName;
                existingContact.email = Contact.email;
                existingContact.street = Contact.street;
                existingContact.city = Contact.city;
                existingContact.phoneNumber = Contact.phoneNumber;
                existingContact.zipCode = Contact.zipCode;
            }
            else
            {
                existingContacts.Add(Contact);
            }

            // Save updated list to file
            var serializedContacts = JsonConvert.SerializeObject(existingContacts, Formatting.Indented);
            _fileService.SaveContentToFile(serializedContacts);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            Debug.WriteLine("Before moving to list view.");
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
            Debug.WriteLine("After moving to list view.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private int GetNextContactId(ObservableCollection<Shared.Models.Contact> contacts)
    {
        // Find next ID in the existing contacts and return the next one
        return contacts?.Any() == true ? contacts.Max(c => c.Id) + 1 : 1;
    }

    [RelayCommand]
    public void EditContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactEditViewModel>();

        ShowSelectedContact();
    }

    [RelayCommand]
    public void SelectPerson()
    {
        // Make sure there is at least one contact in the list.
        if (ContactList.Any())
        {
            SelectedPerson = ContactList.First();
        }
        else
        {
            SelectedPerson = null;
        }
    }

    public void ShowSelectedContact()
    {
        Debug.WriteLine("Entering Showslectedcontact");
        if (SelectedPerson != null)
        {
            var result = GetContactById(SelectedPerson.Id);

            if (result.Status == ServiceStatus.SUCCESSED)
            {
                var contacts = result.Result as List<IContact>;

                if (contacts != null)
                {
                    foreach (var contact in contacts)
                    {
                        Console.WriteLine(contact);
                    }
                }
                else
                {
                    Debug.WriteLine("Result is not of expected type.");
                }
            }
            else
            {
                Debug.WriteLine($"Failed to retrieve contact: {result.Result}");
            }
        }
        else
        {
            Debug.WriteLine("SelectedPerson is null. No contact to show.");
        }
    }

    public IServiceResult GetContactById(int contactId)
    {
        Func<Contact, bool> predicate = contact => contact.Id == contactId;
        var result = _contactRepository.GetContactById(predicate);
        Debug.WriteLine(result);
        return result;
    }

    [RelayCommand] // Does not update the list with new contact at the moment.
    private void NavigateToList()
    {
      var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
      mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}