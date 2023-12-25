using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics; // Import the Debug class
using WpfApp.Services;

namespace WpfApp.Mvvm.ViewModels
{
    public partial class ContactListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ContactService _contactService;

        public ContactListViewModel(IServiceProvider serviceProvider, ContactService contactService)
        {
            _serviceProvider = serviceProvider;
            _contactService = contactService;

            var serviceResult = _contactService.GetAllContacts();

            // Print the type of serviceResult.Result
            Debug.WriteLine("Result Type: " + serviceResult.Result.GetType().FullName);

            // Assuming serviceResult.Result is of type List<IContact>
            if (serviceResult.Result is List<IContact> contacts)
            {
                // Cast each IContact to Contact
                var concreteContacts = contacts.Cast<Shared.Models.Contact>();

                Debug.WriteLine("test");
                ContactList = new ObservableCollection<Shared.Models.Contact>(concreteContacts);
            }
            else
            {
                // Handle the case where the result is not of the expected type
                // You might want to log an error, throw an exception, or handle it in another way
            }
        }



        [ObservableProperty]
        private ObservableCollection<Shared.Models.Contact> contactList = [];

        [RelayCommand]
        private void NavigateToAdd()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
        }

        [RelayCommand]
        private void NavigateToEdit(Contact contact)
        {
            Debug.WriteLine("Just entered navigatetoedit.");
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactEditViewModel>();
            Debug.WriteLine("changed to contacteditviewmodel.");
        }

        [RelayCommand]
        private void Remove(Contact contact)
        {
            _contactService.Remove(contact);
            ContactList = new ObservableCollection<Contact>(_contactService.GetAll());
        }
    }
}
