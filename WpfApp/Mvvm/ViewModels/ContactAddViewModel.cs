using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WpfApp.Services;

namespace WpfApp.Mvvm.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    public readonly ContactService _contactService;

    [ObservableProperty]
    internal ObservableCollection<Shared.Models.Contact> _contactList = [];

    [ObservableProperty]
    private Shared.Models.Contact contact = new();

    public ContactAddViewModel(IServiceProvider serviceProvider, ContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
    }

    [RelayCommand]
     public void Add()
    {
        try
        {
            _contactService.Add(Contact);
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            Debug.WriteLine("Before moving to list view.");
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
            Debug.WriteLine("After moving to list view.");
        }
        catch (Exception ex)
        {
            // Log or display the exception
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    //  if (!string.IsNullOrWhiteSpace(ContactForm.CompanyName) && !string.IsNullOrWhiteSpace(ContactForm.ContactPerson))
    //  {
    //      // ContactList.Add(ContactForm);
    //      ContactForm = new();
    //   }
    // }

    [RelayCommand]
    private void NavigateToList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}
