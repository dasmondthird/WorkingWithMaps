using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using WorkingWithMaps;
using WorkingWithMaps.Droid;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Map = Xamarin.Forms.Maps.Map;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleMap), typeof(CircleMapRenderer))]
namespace WorkingWithMaps.Droid
{
    public class CircleMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _googleMap;
        private CircleMap _circleMap;

        public CircleMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _circleMap = (CircleMap)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap googleMap) // Изменено на protected override
        {
            _googleMap = googleMap;
            UpdateCircle();
        }

        private void UpdateCircle()
        {
            if (_googleMap == null || _circleMap == null) return;

            var circleOptions = new CircleOptions()
                .InvokeCenter(new LatLng(_circleMap.CircleCenter.Latitude, _circleMap.CircleCenter.Longitude))
                .InvokeRadius(_circleMap.CircleRadius)
                .InvokeStrokeColor(_circleMap.CircleStrokeColor.ToAndroid())
                .InvokeFillColor(_circleMap.CircleFillColor.ToAndroid())
                .InvokeStrokeWidth(_circleMap.CircleStrokeWidth);

            _googleMap.AddCircle(circleOptions);
        }
    }
}
