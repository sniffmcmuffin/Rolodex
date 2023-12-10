using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Repositories;
using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
  static void Main()
  {
        Console.WindowWidth = 200; // For the logo

        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<List<IContact>>(new List<IContact>()); 
            services.AddSingleton<IContactRepository, ContactRepository>();
            services.AddSingleton<IContactService, ContactService>();
            services.AddSingleton<IFileService>(provider => new FileService(@"../../../contacts.json"));
            services.AddSingleton<IMenuService, MenuService>();
            services.AddSingleton<MenuService>(); // Register MenuService
        }).Build();


        builder.Start();

        IContact contact = new Contact();
    
        var IMenuService = builder.Services.GetRequiredService<IMenuService>();
      //  var contactService = builder.Services.GetRequiredService<ContactService>();
     //   var contactRepository = builder.Services.GetService<IContactRepository>();

        IMenuService.ShowMenu();
      
        // var contentFile = new FileService(@"contacts.txt");

        // var contactRepository = new ContactRepository(new List<IContact>());
        // var contactService = new ContactService(contactRepository, new FileService(@"../../../contacts.json"));

    }
}
