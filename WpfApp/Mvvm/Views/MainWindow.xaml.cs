using System.Windows;
using WpfApp.Mvvm.ViewModels;

namespace WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel) // Dependency Injection.
    {
        InitializeComponent(); // Alltid längst upp i konstruktorn.
        DataContext = viewModel;
    }
}