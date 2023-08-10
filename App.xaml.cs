using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonsDemo.Service;
using System;
using System.Windows;

namespace PersonsDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddScoped<PersonViewModel>();
               services.AddScoped<IPersonDataService, PersonDataService>();
               services.AddSingleton<PersonWindow>();
           }).Build();

           using(var serviceScope = host.Services.CreateScope())
           {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var personWindow = services.GetRequiredService<PersonWindow>();
                    personWindow.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured" + ex.Message);
                }
           }
        }
    }
}
