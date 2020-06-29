using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DataScienceReport
{
    public static class UserService
    {
        public static IEnumerable<User> Get(IEnumerable<Geolocation> geolocations)
        {
            var ret = new List<User>();
            var groupedGeolocations = geolocations.GroupBy(x => x.Country);

            foreach (var grouping in groupedGeolocations)
            {
                switch (grouping.Key)
                {
                    case "US":
                        ret.AddRange(GetUnitedStatesUsers(grouping.ToList()));
                        break;
                    case "CN":
                        ret.AddRange(GetAsiaUsers(grouping.ToList()));
                        break;
                    case "GB":
                    case "FR":
                    case "DE":
                        ret.AddRange(GetEuropeUsers(grouping.ToList()));
                        break;
                }
            }

            return ret;
        }

        private static IEnumerable<User> GetUnitedStatesUsers(List<Geolocation> geolocations)
        {
            var url = @"https://cs-interview-users-us.azurewebsites.net/api/UserDataFunction?code=Ax1P8UqUd6sQtQpJ4ldX8wbh/cEXt6xDFO4IK3ptTlDC29KlGHyj6A==";
            var ret = GetUsers(url, geolocations);
            foreach (var item in ret)
            {
                item.Country = geolocations.Single(x => x.Address == item.Address).Country;
            }
            return ret;
        }

        private static IEnumerable<User> GetEuropeUsers(List<Geolocation> geolocations)
        {
            var url = @"https://cs-interview-users-eu.azurewebsites.net/api/UserDataFunction?code=GjLpRZ2u2ZNoNi0Rrvdl6PSF3XEWUgJUY8e6BqUOhgx9e5KRISJuQA==";
            var ret = GetUsers(url, geolocations);
            foreach (var item in ret)
            {
                item.Country = geolocations.Single(x => x.Address == item.Address).Country;
            }
            return ret;
        }

        private static IEnumerable<User> GetAsiaUsers(List<Geolocation> geolocations)
        {
            var url = @" https://cs-interview-users-as.azurewebsites.net/api/UserDataFunction?code=wUi5HeaRJtYjX/7APOVehBWvLLaOYI7VFJBRCCX5tmk6i2aOoe0nkA==";
            var ret = GetUsers(url, geolocations);
            foreach (var item in ret)
            {
                item.Country = geolocations.Single(x => x.Address == item.Address).Country;
            }
            return ret;
        }
        
        private static IEnumerable<User> GetUsers(string url, List<Geolocation> geolocations)
        {
            var ipAddresses = geolocations.Select(geolocation => geolocation.Address).ToList();
            var json = JsonConvert.SerializeObject(new IpAddressList(ipAddresses.ToList()));
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            var bytes = Encoding.ASCII.GetBytes(json);
            request.ContentLength = bytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
            if (response == null) return new List<User>();

            var ret = new UserList();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var j = reader.ReadToEnd().Trim();
                ret = JsonConvert.DeserializeObject<UserList>(j);
            }

            return ret.Users;
        }
    }
}