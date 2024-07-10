using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace WorkingWithMaps
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ICommand NavigateCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                try
                {
                    Page page = (Page)Activator.CreateInstance(pageType);
                    await Navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    LogException("Navigation", ex);
                }
            });

            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Title = "Типы карт", Description = "Отображение карты с различными типами.", Icon = "map_icon.png", PageType = typeof(MapTypesPage) },
                new MenuItem { Title = "Карта", Description = "Отображение карты с другими свойствами.", Icon = "map_icon.png", PageType = typeof(MapPropertiesPage) },
                new MenuItem { Title = "Пины", Description = "Добавление пинов на карту.", Icon = "pin_icon.png", PageType = typeof(PinPage) },
                new MenuItem { Title = "Пины с привязкой данных", Description = "Добавление коллекции пинов на карту.", Icon = "pin_bind_icon.png", PageType = typeof(PinItemsSourcePage) },
                new MenuItem { Title = "Маршруты", Description = "Управление и отображение маршрутов на карте.", Icon = "route_icon.png", PageType = typeof(RoutePage) }
            };

            BindingContext = this;
        }

        private void LogException(string context, Exception ex)
        {
            Console.WriteLine($"Error during {context}: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                Console.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
            }
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public Type PageType { get; set; }
    }
}
