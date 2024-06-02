using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public class PinPageCode : ContentPage
    {
        public PinPageCode()
        {
            Title = "Pins Demo";

            var position = new Position(36.9628066, -122.0194722);
            var map = CreateMap(position);

            var button = new Button { Text = "Add more pins" };
            button.Clicked += (sender, e) => AddPins(map);

            Content = new StackLayout
            {
                Margin = new Thickness(10),
                Children = { map, button }
            };
        }

        private Map CreateMap(Position position)
        {
            var mapSpan = new MapSpan(position, 0.01, 0.01);
            var map = new Map(mapSpan);

            var pin = new Pin
            {
                Label = "Santa Cruz",
                Address = "The city with a boardwalk",
                Type = PinType.Place,
                Position = position
            };
            map.Pins.Add(pin);

            return map;
        }

        private void AddPins(Map map)
        {
            var boardwalkPin = CreatePin("Boardwalk", "Santa Cruz", new Position(36.9641949, -122.0177232));
            boardwalkPin.MarkerClicked += OnMarkerClickedAsync;

            var wharfPin = CreatePin("Wharf", "Santa Cruz", new Position(36.9571571, -122.0173544));
            wharfPin.InfoWindowClicked += OnInfoWindowClickedAsync;

            map.Pins.Add(boardwalkPin);
            map.Pins.Add(wharfPin);
        }

        private Pin CreatePin(string label, string address, Position position)
        {
            return new Pin
            {
                Position = position,
                Label = label,
                Address = address,
                Type = PinType.Place
            };
        }

        private async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
        {
            e.HideInfoWindow = true;
            var pinName = ((Pin)sender).Label;
            await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
        }

        private async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {
            var pinName = ((Pin)sender).Label;
            await DisplayAlert("Info Window Clicked", $"The info window was clicked for {pinName}.", "Ok");
        }
    }
}
