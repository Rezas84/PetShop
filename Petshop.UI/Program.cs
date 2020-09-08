using System;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.AplicationServices.Interfaces;
using PetShop.Core.DomainServices.Interfaces;
using PetShop.Core.DomainServices.Services;
using PetShop.UI.Services;

namespace PetShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var ServiceCollection = new ServiceCollection();
            ServiceCollection.AddScoped<IPetRepository, PetRepository>();
            ServiceCollection.AddScoped<IPetValidatorService, PetValidatorService>();
            ServiceCollection.AddScoped<IPetService, PetService>();
            ServiceCollection.AddScoped<IPrinterService, PrinterService>();

            var ServiceProvider = ServiceCollection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<IPetRepository>().Init();
            var Printer = ServiceProvider.GetRequiredService<IPrinterService>();
            Printer.Run();
        }
    }
}
