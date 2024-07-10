using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WorkingWithMaps.Services
{
    public class RouteService
    {
        private readonly string _apiKey;

        public RouteService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetRouteAsync(string origin, string destination)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={_apiKey}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
