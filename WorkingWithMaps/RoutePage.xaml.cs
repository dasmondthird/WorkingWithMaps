using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using WorkingWithMaps.ViewModels;

namespace WorkingWithMaps
{
    public partial class RoutePage : ContentPage
    {
        private readonly RouteViewModel _viewModel;

        public RoutePage()
        {
            InitializeComponent();
            _viewModel = (RouteViewModel)BindingContext;

            _viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(RouteViewModel.RoutePositions))
                {
                    UpdateRouteOnMap();
                }
            };

            UpdateRouteOnMap();
        }

        private void UpdateRouteOnMap()
        {
            map.MapElements.Clear();
            var polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 12
            };

            foreach (var position in _viewModel.RoutePositions)
            {
                polyline.Geopath.Add(position);
            }

            map.MapElements.Add(polyline);

            if (_viewModel.RoutePositions.Any())
            {
                var firstPosition = _viewModel.RoutePositions.First();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(firstPosition, Distance.FromMiles(10)));
            }
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            _viewModel.AddPositionCommand.Execute(e.Position);
        }
    }
}
