using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
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
using WpfApp.Services;


namespace WpfApp.Mvvm.ViewModels
{
    public partial class ContactEditViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ContactService _contactService;
        private readonly IContactRepository _contactRepository;

        private Shared.Models.Contact? _contactForm;
        private Shared.Models.Contact? _selectedPerson;

        private ObservableCollection<Shared.Models.Contact> _contactList;

      
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

        public ContactEditViewModel(IServiceProvider serviceProvider, ContactService contactService)
        {
            _serviceProvider = serviceProvider;
            _contactService = contactService;
            _contactRepository = _serviceProvider.GetRequiredService<IContactRepository>();
            _contactList = new ObservableCollection<Shared.Models.Contact>();

            ShowSelectedContact();           
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
            SelectedPerson = _contactList.FirstOrDefault();
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
                        Debug.WriteLine("Result is not of the expected type.");
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
            var contact = _contactRepository.GetContactById(contactId);
            Debug.WriteLine(contact);
            return (contact);            
        }

        [RelayCommand]
        private void NavigateToList()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
        }
    }
}