using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps
{
    public partial class MapRegionPage : ContentPage
    {
        public MapRegionPage()
        {
            InitializeComponent();
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            DisplayAlert("Координаты", $"Широта: {e.Position.Latitude}, Долгота: {e.Position.Longitude}", "Ок");
        }
    }
}
