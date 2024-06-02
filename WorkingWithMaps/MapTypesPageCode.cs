using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public class MapTypesPageCode : ContentPage
    {
        private Map _map;

        public MapTypesPageCode()
        {
            Title = "Map Types Demo";

            _map = new Map();

            
            var slider = CreateZoomSlider();
            var buttons = CreateMapTypeButtons();

            
            Content = new StackLayout
            {
                Children = { _map, slider, buttons }
            };
        }

        private Slider CreateZoomSlider()
        {
            var slider = new Slider(1, 18, 12)
            {
                Margin = new Thickness(20, 0, 20, 0)
            };
            slider.ValueChanged += OnSliderValueChanged;
            return slider;
        }

        private StackLayout CreateMapTypeButtons()
        {
            var streetButton = new Button { Text = "Street" };
            var satelliteButton = new Button { Text = "Satellite" };
            var hybridButton = new Button { Text = "Hybrid" };

            streetButton.Clicked += OnButtonClicked;
            satelliteButton.Clicked += OnButtonClicked;
            hybridButton.Clicked += OnButtonClicked;

            return new StackLayout
            {
                Spacing = 30,
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Children = { streetButton, satelliteButton, hybridButton }
            };
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double zoomLevel = e.NewValue;
            double latlongDegrees = 360 / Math.Pow(2, zoomLevel);
            if (_map.VisibleRegion != null)
            {
                _map.MoveToRegion(new MapSpan(_map.VisibleRegion.Center, latlongDegrees, latlongDegrees));
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Text)
                {
                    case "Street":
                        _map.MapType = MapType.Street;
                        break;
                    case "Satellite":
                        _map.MapType = MapType.Satellite;
                        break;
                    case "Hybrid":
                        _map.MapType = MapType.Hybrid;
                        break;
                }
            }
        }
    }
}
