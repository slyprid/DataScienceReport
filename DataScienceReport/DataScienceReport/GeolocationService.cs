using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DataScienceReport
{
    public static class GeolocationService
    {
        private static string Url = @"https://cs-interview-geolocation.azurewebsites.net/api/GeolocationFunction?code=h/0av4gxkkTRz6d5HFIKr/U3b4ynT/CCNiKuHI7Wk7pVWYBKS8iZOw==";

        public static IEnumerable<Geolocation> Get(IEnumerable<string> ipAddresses)
        {
            var json = JsonConvert.SerializeObject(new IpAddressList(ipAddresses.ToList()));
            var request = WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";
            var bytes = Encoding.ASCII.GetBytes(json);
            request.ContentLength = bytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
            if(response == null) return new List<Geolocation>();

            var ret = new GeolocationList();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                ret = JsonConvert.DeserializeObject<GeolocationList>(reader.ReadToEnd().Trim());
            }

            return ret.Geolocations;
        }
    }
}
