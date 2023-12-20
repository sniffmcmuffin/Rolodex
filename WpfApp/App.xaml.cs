using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfApp.Mvvm.ViewModels;
using WpfApp.Mvvm.Views;

namespace WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        })
        .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        builder!.Start();

        var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

       // base.OnStartup(e); Standardfunktioner om man behöver dom.
    }
}
