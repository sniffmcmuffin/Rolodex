using CommunityToolkit.Mvvm.ComponentModel;
using Shared.Models;
using System.Collections.ObjectModel;

namespace WpfApp.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Shared.Models.Contact _contactForm = new(); // Privat fält.   

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = [];
}
