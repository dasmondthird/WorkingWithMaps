using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace WorkingWithMaps.ViewModels
{
    public class RouteViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Position> _routePositions;

        public IEnumerable<Position> RoutePositions => _routePositions;

        public ICommand AddPositionCommand { get; }
        public ICommand ClearPositionsCommand { get; }

        public RouteViewModel()
        {
            _routePositions = new ObservableCollection<Position>
            {
                new Position(40.67, -73.94), 
                new Position(34.11, -118.41),
                new Position(37.77, -122.45)
            };

            AddPositionCommand = new Command<Position>(AddPosition);
            ClearPositionsCommand = new Command(() => _routePositions.Clear());
        }

        private void AddPosition(Position position)
        {
            _routePositions.Add(position);
            OnPropertyChanged(nameof(RoutePositions));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
