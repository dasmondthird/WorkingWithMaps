using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingWithMaps.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace WorkingWithMaps.ViewModels
{
    public class RouteMapViewModel : BindableObject
    {
        private readonly RouteService _routeService;
        private string _origin;
        private string _destination;

        public ObservableCollection<Position> RoutePositions { get; }
        public ICommand FetchRouteCommand { get; }

        public string Origin
        {
            get => _origin;
            set
            {
                _origin = value;
                OnPropertyChanged();
            }
        }

        public string Destination
        {
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged();
            }
        }

        public RouteMapViewModel()
        {
            _routeService = (RouteService)App.ServiceProvider.GetService(typeof(RouteService));
            RoutePositions = new ObservableCollection<Position>();
            FetchRouteCommand = new Command(async () => await FetchRouteAsync());
        }

        private async Task FetchRouteAsync()
        {
            var routeJson = await _routeService.GetRouteAsync(Origin, Destination);
            var routeObject = JObject.Parse(routeJson);
            var points = routeObject["routes"][0]["overview_polyline"]["points"].ToString();
            var positions = DecodePolyline(points);
            RoutePositions.Clear();
            foreach (var position in positions)
            {
                RoutePositions.Add(position);
            }
        }

        private IEnumerable<Position> DecodePolyline(string encodedPoints)
        {
            if (string.IsNullOrWhiteSpace(encodedPoints))
                yield break;

            int index = 0, lat = 0, lng = 0;

            while (index < encodedPoints.Length)
            {
                int b, shift = 0, result = 0;
                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;

                shift = 0;
                result = 0;
                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;

                yield return new Position(
                    Convert.ToDouble(lat) / 1E5,
                    Convert.ToDouble(lng) / 1E5);
            }
        }
    }
}
