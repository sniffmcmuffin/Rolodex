using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Interfaces;
using Shared.Repositories;
using Shared.Services;
using System.Windows;
using System.IO;
using WpfApp.Mvvm.ViewModels;
using WpfApp.Mvvm.Views;
using WpfApp.Services;

namespace WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "Shared", "contacts.json");

        builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<IContactRepository, ContactRepository>();
            
            services.AddSingleton<IFileService>(provider => new FileService(@"../../../../Shared/contacts.json"));
            services.AddTransient<ContactService>(provider => new ContactService(provider.GetRequiredService<IContactRepository>(), @"../../../../Shared/contacts.json"));

            services.AddTransient<ContactListViewModel>();
            services.AddTransient<ContactListView>();

            services.AddTransient<ContactAddViewModel>();
            services.AddTransient<ContactAddView>();

            services.AddTransient<ContactEditViewModel>();
            services.AddTransient<ContactEditView>();

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