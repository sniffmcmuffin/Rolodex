using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace WpfApp.Mvvm.ViewModels;

public partial class ContactEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<Shared.Models.Contact> _contactList = [];

    [ObservableProperty]
    private Shared.Models.Contact _contactForm = new();

    public ContactEditViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    public void EditContact()
    {
       var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
       mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }

    [RelayCommand]
    private void NavigateToList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}
