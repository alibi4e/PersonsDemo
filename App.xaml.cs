using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonsDemo.DataParser;
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
        #region Private Members
        private IHost? host;
        #endregion

        #region Overrides
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            host = Host.CreateDefaultBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddScoped<PersonViewModel>();
               services.AddScoped<IPersonCsvParser, PersonCsvParser>();
               services.AddScoped<IPersonDataService, PersonDataService>();
               services.AddSingleton<PersonWindow>();
           }).Build();

            using (var serviceScope = host.Services.CreateScope())
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
        #endregion
    }
}
