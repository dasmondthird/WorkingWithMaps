using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public partial class MapPropertiesPage : ContentPage
    {
        public MapPropertiesPage()
        {
            InitializeComponent();
            scrollEnabledCheckBox.CheckedChanged += OnCheckBoxCheckedChanged;
            zoomEnabledCheckBox.CheckedChanged += OnCheckBoxCheckedChanged;
            moveRegionCheckBox.CheckedChanged += OnCheckBoxCheckedChanged;
        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            switch (checkBox.StyleId)
            {
                case "scrollEnabledCheckBox":
                    map.HasScrollEnabled = scrollEnabledCheckBox.IsChecked;
                    break;
                case "zoomEnabledCheckBox":
                    map.HasZoomEnabled = zoomEnabledCheckBox.IsChecked;
                    break;
                case "showUserCheckBox":
                    map.IsShowingUser = showUserCheckBox.IsChecked;
                    break;
                case "showTrafficCheckBox":
                    map.TrafficEnabled = showTrafficCheckBox.IsChecked;
                    break;
                case "moveRegionCheckBox":
                    map.MoveToLastRegionOnLayoutChange = moveRegionCheckBox.IsChecked;
                    break;
            }
        }

        void OnAddCircleClicked(object sender, EventArgs e)
        {
            if (double.TryParse(circleRadiusEntry.Text, out double radius))
            {
                var circle = new Circle
                {
                    Center = map.VisibleRegion.Center,
                    Radius = new Distance(radius),
                    StrokeColor = Color.Blue,
                    StrokeWidth = 2,
                    FillColor = Color.FromRgba(0, 0, 255, 32)
                };
                map.MapElements.Add(circle);
            }
            else
            {
                DisplayAlert("Ошибка", "Некорректный радиус", "Ок");
            }
        }
    }
}
