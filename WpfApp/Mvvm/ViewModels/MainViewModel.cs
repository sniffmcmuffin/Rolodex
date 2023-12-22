using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using System.Collections.ObjectModel;

namespace WpfApp.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Shared.Models.Contact _contactForm = new();    

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = [];

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();

    }

    [RelayCommand] 
    public void AddContactToList()
    {
       if (!string.IsNullOrWhiteSpace(ContactForm.CompanyName) && !string.IsNullOrWhiteSpace(ContactForm.ContactPerson))
        {
            ContactList.Add(ContactForm);
            ContactForm = new();
        }
    }

    [RelayCommand]
    public void RemoveContactFromList(Contact contact)
    {
        if (contact != null)
        {
            ContactList.Remove(contact);
           // ContactList = ContactList.OrderBy(x => x.CompanyName).ToList(); Fix sort later on.
        }
    }

}
