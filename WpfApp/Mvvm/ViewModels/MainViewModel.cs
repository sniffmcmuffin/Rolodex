using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp.Mvvm.Models;

namespace WpfApp.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject   
{
    [ObservableProperty]
    private Contact contactForm = new(); // Privat fält
}
