using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using WorkingWithMaps.ViewModels;

namespace WorkingWithMaps
{
    public partial class RoutePage : ContentPage
    {
        public RoutePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RouteMapViewModel viewModel)
            {
                viewModel.RoutePositions.CollectionChanged += (s, e) => UpdateMapRoute();
            }
        }

        private void UpdateMapRoute()
        {
            if (BindingContext is RouteMapViewModel viewModel && viewModel.RoutePositions.Any())
            {
                map.MapElements.Clear();
                var polyline = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 5
                };

                foreach (var position in viewModel.RoutePositions)
                {
                    polyline.Geopath.Add(position);
                }

                map.MapElements.Add(polyline);

                // Calculate the bounding box
                var minLat = viewModel.RoutePositions.Min(p => p.Latitude);
                var maxLat = viewModel.RoutePositions.Max(p => p.Latitude);
                var minLon = viewModel.RoutePositions.Min(p => p.Longitude);
                var maxLon = viewModel.RoutePositions.Max(p => p.Longitude);

                var centerLat = (minLat + maxLat) / 2;
                var centerLon = (minLon + maxLon) / 2;
                var latDelta = maxLat - minLat;
                var lonDelta = maxLon - minLon;

                var span = new MapSpan(new Position(centerLat, centerLon), latDelta, lonDelta);
                map.MoveToRegion(span);
            }
        }
    }
}
