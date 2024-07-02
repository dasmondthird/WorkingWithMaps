using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using WorkingWithMaps.Models;
using WorkingWithMaps.Services;

namespace WorkingWithMaps.ViewModels
{
    public class RouteMapViewModel : INotifyPropertyChanged
    {
        private string _startAddress;
        private string _endAddress;
        private Polyline _route;
        private bool _isLoading;

        public string StartAddress
        {
            get => _startAddress;
            set
            {
                _startAddress = value;
                OnPropertyChanged(nameof(StartAddress));
            }
        }

        public string EndAddress
        {
            get => _endAddress;
            set
            {
                _endAddress = value;
                OnPropertyChanged(nameof(EndAddress));
            }
        }

        public Polyline Route
        {
            get => _route;
            set
            {
                _route = value;
                OnPropertyChanged(nameof(Route));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand BuildRouteCommand { get; }
        public ICommand ClearRouteCommand { get; }

        public RouteMapViewModel()
        {
            BuildRouteCommand = new Command(async () => await BuildRoute());
            ClearRouteCommand = new Command(ClearRoute);
        }

        private async Task BuildRoute()
        {
            IsLoading = true;
            var startPosition = await GeocodeAddressAsync(StartAddress);
            var endPosition = await GeocodeAddressAsync(EndAddress);

            if (startPosition == null || endPosition == null)
            {
                IsLoading = false;
                await App.Current.MainPage.DisplayAlert("Error", "Invalid start or end address", "OK");
                return;
            }

            var routePositions = await RouteBuilder.GetRouteAsync(startPosition, endPosition);
            Route = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 5
            };

            foreach (var position in routePositions)
            {
                Route.Geopath.Add(position);
            }

            IsLoading = false;
            MessagingCenter.Send(this, "UpdateRoute", Route);
        }

        private void ClearRoute()
        {
            Route = null;
            StartAddress = string.Empty;
            EndAddress = string.Empty;
            MessagingCenter.Send(this, "ClearRoute");
        }

        private async Task<Position> GeocodeAddressAsync(string address)
        {
            var geoCoder = new Geocoder();
            var positions = await geoCoder.GetPositionsForAddressAsync(address);
            return positions.FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
