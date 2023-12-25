﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics; 
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
                      
            Debug.WriteLine("Result Type: " + serviceResult.Result.GetType().FullName);

            if (serviceResult.Result is List<IContact> contacts)
            {
                // Cast each IContact to Contact
                var concreteContacts = contacts.Cast<Shared.Models.Contact>();

                Debug.WriteLine("test");
                ContactList = new ObservableCollection<Shared.Models.Contact>(concreteContacts);
            }
            else
            {
                // Handle case where result is not of expected type with some logging.               
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
          
            var contactEditViewModel = _serviceProvider.GetRequiredService<ContactEditViewModel>();

            if (contact != null)
            {
                Debug.WriteLine($"Setting SelectedPerson to: {contact}");
                contactEditViewModel.SelectedPerson = contact;
            }
            else
            {
                Debug.WriteLine("The 'contact' parameter is null.");
            }
                     
            Debug.WriteLine($"SelectedPerson after setting: {contactEditViewModel.SelectedPerson}");

            // Change CurrentViewModel to ContactEditViewModel
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactEditViewModel;

            Debug.WriteLine("Changed to ContactEditViewModel.");
        }

        [RelayCommand]
        private void Remove(Contact contact)
        {
            _contactService.Remove(contact);
            ContactList = new ObservableCollection<Contact>(_contactService.GetAll());
        }
    }
}