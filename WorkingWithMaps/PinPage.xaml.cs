using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public partial class PinPage : ContentPage
    {
        ObservableCollection<Pin> pinsCollection;

        public PinPage()
        {
            InitializeComponent();
            pinsCollection = new ObservableCollection<Pin>();
            pinsListView.ItemsSource = pinsCollection;
        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            var pin = new Pin
            {
                Position = e.Position,
                Label = "Пользовательский пин",
                Address = $"Широта: {e.Position.Latitude}, Долгота: {e.Position.Longitude}",
                Type = PinType.Place
            };

            map.Pins.Add(pin);
            pinsCollection.Add(pin);
            DisplayAlert("Пин добавлен", $"Широта: {e.Position.Latitude}, Долгота: {e.Position.Longitude}", "Ок");
        }

        void OnRemovePinsClicked(object sender, EventArgs e)
        {
            map.Pins.Clear();
            pinsCollection.Clear();
            DisplayAlert("Пины удалены", "Все пины были удалены", "Ок");
        }

        void OnAddPinsClicked(object sender, EventArgs e)
        {
            var boardwalkPin = new Pin
            {
                Position = new Position(36.9641949, -122.0177232),
                Label = "Boardwalk",
                Address = "Santa Cruz",
                Type = PinType.Place
            };
            boardwalkPin.MarkerClicked += OnMarkerClickedAsync;

            var wharfPin = new Pin
            {
                Position = new Position(36.9571571, -122.0173544),
                Label = "Wharf",
                Address = "Santa Cruz",
                Type = PinType.Place
            };
            wharfPin.InfoWindowClicked += OnInfoWindowClickedAsync;

            map.Pins.Add(boardwalkPin);
            map.Pins.Add(wharfPin);

            pinsCollection.Add(boardwalkPin);
            pinsCollection.Add(wharfPin);

            DisplayAlert("Пины добавлены", "Добавлены стандартные пины: Boardwalk и Wharf", "Ок");
        }

        async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
        {
            e.HideInfoWindow = true;
            var pinName = ((Pin)sender).Label;
            await DisplayAlert("Пин нажата", $"{pinName} был нажат.", "Ок");
        }

        async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {
            var pinName = ((Pin)sender).Label;
            await DisplayAlert("Информационное окно нажата", $"Информационное окно для {pinName} было нажато.", "Ок");
        }
    }
}
