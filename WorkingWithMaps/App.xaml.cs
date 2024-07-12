using Microsoft.Extensions.DependencyInjection;
using System;
using WorkingWithMaps.Services;
using WorkingWithMaps.ViewModels;
using Xamarin.Forms;

namespace WorkingWithMaps
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            ConfigureServices();
            MainPage = new NavigationPage(new MainPage());
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<RouteService>(provider => new RouteService(""));
            services.AddTransient<RouteMapViewModel>();
            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
