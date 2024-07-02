using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkingWithMaps.ViewModels;

namespace WorkingWithMaps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteMapPage : ContentPage
    {
        public RouteMapPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<RouteMapViewModel, Polyline>(this, "UpdateRoute", (sender, polyline) =>
            {
                routeMap.MapElements.Clear();
                routeMap.MapElements.Add(polyline);
                if (polyline.Geopath.Count > 0)
                {
                    routeMap.MoveToRegion(MapSpan.FromCenterAndRadius(polyline.Geopath[0], Distance.FromKilometers(10)));
                }
            });

            MessagingCenter.Subscribe<RouteMapViewModel>(this, "ClearRoute", sender =>
            {
                routeMap.MapElements.Clear();
            });
        }
    }
}
