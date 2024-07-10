using Xamarin.Forms.Maps;

namespace WorkingWithMaps.Models
{
    public class Region
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Polygon Boundary { get; set; }
    }
}
