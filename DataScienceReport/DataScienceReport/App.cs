using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataScienceReport
{
    public class App
        : IDisposable
    {
        #region Fields / Variables

        private string _inputFilename;
        private List<string> _ipAddresses;
        private List<Geolocation> _geolocations;
        private List<User> _users;

        #endregion

        #region Initialization

        public App()
        {
            _ipAddresses = new List<string>();
            _geolocations = new List<Geolocation>();
            _users = new List<User>();
        }

        #endregion

        #region Dispose

        public void Dispose() { }

        #endregion

        #region Methods

        public void Run(string inputFilename)
        {
            _inputFilename = inputFilename;

            ValidateInputFilename();
            ReadInputFile();

            GetGeolocations();

            GetUserData();

            GenerateOutput();

            WaitForInput();
        }

        private void WaitForInput()
        {
            #if DEBUG

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            #endif
        }

        private void ValidateInputFilename()
        {
            if (!File.Exists(_inputFilename))
            {
                Console.WriteLine($"ERR >> Input file doesn't exists [{_inputFilename}]");
                WaitForInput();
                Environment.Exit(-1);
            }
        }

        private void ReadInputFile()
        {
            _ipAddresses = File.ReadAllLines(_inputFilename).ToList();

            Console.WriteLine($">> {_ipAddresses.Count} IP Addresses loaded");
        }

        private void GetGeolocations()
        {
            _geolocations = GeolocationService.Get(_ipAddresses).ToList();

            Console.WriteLine($">> {_geolocations.Count} Geolocations found");
        }

        private void GetUserData()
        {
            _users = UserService.Get(_geolocations).ToList();

            Console.WriteLine($">> {_users.Count} users found");
        }

        private void GenerateOutput()
        {
            var sb = new StringBuilder();

            foreach (var user in _users)
            {
                sb.AppendLine($"{user.Address},{user.Country},{user.Name},{user.Gender},{user.Age}");
            }

            File.WriteAllText("./report.csv", sb.ToString());

            Console.WriteLine($">> Report generated [report.csv]");
        }

        #endregion
    }
}