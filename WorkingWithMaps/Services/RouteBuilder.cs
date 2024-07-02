using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Maps;

namespace WorkingWithMaps.Services
{
    public static class RouteBuilder
    {
        private const string ApiKey = "AIzaSyCLW5ILryG1X42xxJxMZtw1r4jbEEEXVhQ"; 

        public static async Task<IEnumerable<Position>> GetRouteAsync(Position start, Position end)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={start.Latitude},{start.Longitude}&destination={end.Latitude},{end.Longitude}&key={ApiKey}";
                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);

                var route = new List<Position>();
                var steps = json["routes"][0]["legs"][0]["steps"];

                foreach (var step in steps)
                {
                    var startLat = step["start_location"]["lat"].Value<double>();
                    var startLng = step["start_location"]["lng"].Value<double>();
                    route.Add(new Position(startLat, startLng));

                    var endLat = step["end_location"]["lat"].Value<double>();
                    var endLng = step["end_location"]["lng"].Value<double>();
                    route.Add(new Position(endLat, endLng));
                }

                return route;
            }
        }
    }
}
