using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Interfaces;
using Shared.Models;
using Shared.Repositories;
using Shared.Services;

using KonsolApp.Interfaces;
using KonsolApp.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KonsolApp;

class Program
{
    static void Main()
    {
       // Console.WindowWidth = 200; // For the logo

        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<List<IContact>>(new List<IContact>());
            services.AddSingleton<IContactRepository, ContactRepository>();
            //   services.AddSingleton<IContactService, ContactService>(); // Why did this stopped woring for no apparent reason?
            services.AddSingleton<ISharedContactService>(provider => new SharedContactService(provider.GetRequiredService<IContactRepository>(),
            @"../../../../Shared/contacts.json"));

            //    services.AddSingleton<IFileService>(provider => new FileService(@"../../../contacts.json"));
            services.AddSingleton<IFileService>(provider => new FileService(@"../../../../Shared/contacts.json"));
            services.AddScoped<IMenuService, MenuService>();
            services.AddSingleton<MenuService>();

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