using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace WpfApp.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    [ObservableProperty]
    private ObservableObject currentViewModel = null!;

    //[ObservableProperty]
    //private Shared.Models.Contact _contactForm = new();    
    
    //[ObservableProperty]
    //private ObservableObject? _currentViewModel;

    



    //[RelayCommand] 
    //public void AddContactToList()
    //{
    //   // var con = AddCon.FirstOrDefault(x => x.Id == Contact.Id);

    //   if (!string.IsNullOrWhiteSpace(ContactForm.CompanyName) && !string.IsNullOrWhiteSpace(ContactForm.ContactPerson))
    //    {            
    //       // ContactList.Add(ContactForm);
    //        ContactForm = new();
    //    }
    //}

    //[RelayCommand]
    //private void Edit()    {       
    //}

    //[RelayCommand]
    //public void RemoveContactFromList(Contact contact)
    //{
    //    if (contact != null)
    //    {
    //       // ContactList.Remove(contact);
    //        // Make an id check so the correct item is removed.
    //       // ContactList = ContactList.OrderBy(x => x.CompanyName).ToList(); Fix sort later on.
    //    }
    //}

}
