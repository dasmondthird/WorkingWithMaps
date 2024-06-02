using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public class PolygonsPageCode : ContentPage
    {
        private Map _map;
        private Polyline _interstateBridge;
        private Polygon _msWest;
        private Polygon _msEast;

        public PolygonsPageCode()
        {
            Title = "Polygon/Polyline Code Demo";
            _map = new Map();

            var polygonButton = new Button { Text = "Show Campus (Polygons)" };
            polygonButton.Clicked += AddPolygonsClicked;

            var polylineButton = new Button { Text = "Show Access Road (Polyline)" };
            polylineButton.Clicked += AddPolylineClicked;

            var clearButton = new Button { Text = "Clear All" };
            clearButton.Clicked += ClearClicked;

            InitializeMapElements();

            Content = new StackLayout
            {
                Children = { _map, polygonButton, polylineButton, clearButton }
            };

            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.640663, -122.1376177), Distance.FromMiles(1)));
        }

        private void InitializeMapElements()
        {
            _msWest = new Polygon
            {
                StrokeColor = Color.FromHex("#FF9900"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#88FF9900"),
                Geopath =
                {
                    new Position(47.6458676, -122.1356007),
                    new Position(47.6458097, -122.142789),
                    new Position(47.6367593, -122.1428104),
                    new Position(47.6368027, -122.1398707),
                    new Position(47.6380172, -122.1376177),
                    new Position(47.640663, -122.1352359),
                    new Position(47.6426148, -122.1347209),
                    new Position(47.6458676, -122.1356007)
                }
            };

            _msEast = new Polygon
            {
                StrokeColor = Color.FromHex("#1BA1E2"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#881BA1E2"),
                Geopath =
                {
                    new Position(47.6368678, -122.137305),
                    new Position(47.6368894, -122.134655),
                    new Position(47.6359424, -122.134655),
                    new Position(47.6359496, -122.1325521),
                    new Position(47.6424124, -122.1325199),
                    new Position(47.642463, -122.1338932),
                    new Position(47.6406414, -122.1344833),
                    new Position(47.6384943, -122.1361248),
                    new Position(47.6372943, -122.1376912),
                    new Position(47.6368678, -122.137305)
                }
            };

            _interstateBridge = new Polyline
            {
                StrokeColor = Color.Black,
                StrokeWidth = 12,
                Geopath =
                {
                    new Position(47.6381401, -122.1317367),
                    new Position(47.6381473, -122.1350841),
                    new Position(47.6382847, -122.1353094),
                    new Position(47.6384582, -122.1354703),
                    new Position(47.6401136, -122.1360819),
                    new Position(47.6403883, -122.1364681),
                    new Position(47.6407426, -122.1377019),
                    new Position(47.6412558, -122.1404056),
                    new Position(47.6414148, -122.1418647),
                    new Position(47.6414654, -122.1432702)
                }
            };
        }

        private void AddPolylineClicked(object sender, EventArgs e)
        {
            if (!_map.MapElements.Contains(_interstateBridge))
            {
                _map.MapElements.Add(_interstateBridge);
            }
        }

        private void AddPolygonsClicked(object sender, EventArgs e)
        {
            if (!_map.MapElements.Contains(_msWest))
            {
                _map.MapElements.Add(_msWest);
            }

            if (!_map.MapElements.Contains(_msEast))
            {
                _map.MapElements.Add(_msEast);
            }
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            _map.MapElements.Remove(_msWest);
            _map.MapElements.Remove(_msEast);
            _map.MapElements.Remove(_interstateBridge);
        }
    }
}
