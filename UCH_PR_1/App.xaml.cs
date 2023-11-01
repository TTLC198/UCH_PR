using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UCH_PR_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {/*
                    services.AddDbContext<ApplicationContext>(options =>
                    {
                        options.UseSqlServer("Data Source=,;User ID=;Password=;TrustServerCertificate=True;Database=RPM_PR_DB_1;");
                    });*/
                }).Build();
        }
    }
}