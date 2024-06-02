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
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            });

            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Title = "Типы карт", Description = "Отображение карты с различными типами.", Icon = "map_icon.png", PageType = typeof(MapTypesPage) },
                new MenuItem { Title = "Регион карты", Description = "Отображение карты в конкретном месте.", Icon = "region_icon.png", PageType = typeof(MapRegionPage) },
                new MenuItem { Title = "Карта", Description = "Отображение карты с другими свойствами.", Icon = "map_icon.png", PageType = typeof(MapPropertiesPage) },
                new MenuItem { Title = "Пины", Description = "Добавление пинов на карту.", Icon = "pin_icon.png", PageType = typeof(PinPage) },
                new MenuItem { Title = "Пины с привязкой данных", Description = "Добавление коллекции пинов на карту.", Icon = "pin_bind_icon.png", PageType = typeof(PinItemsSourcePage) },
                new MenuItem { Title = "Полигон и полилиния", Description = "Добавление полигонов и полилиний на карту.", Icon = "polygon_icon.png", PageType = typeof(PolygonsPage) },
                new MenuItem { Title = "Выделение круглой области", Description = "Добавление круга на карту.", Icon = "circle_icon.png", PageType = typeof(CirclePage) },
                new MenuItem { Title = "Геокодер", Description = "Геокодирование и обратное геокодирование адреса.", Icon = "geocoder_icon.png", PageType = typeof(GeocoderPage) },
                new MenuItem { Title = "Приложение карты", Description = "Запуск встроенного приложения карт.", Icon = "native_map_icon.png", PageType = typeof(MapAppPage) }
            };

            BindingContext = this;
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
