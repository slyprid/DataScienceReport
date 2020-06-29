using System;

namespace DataScienceReport
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0) args = new[] { "./Input/ipaddresses.txt" };
            using (var app = new App()) app.Run(args[0]);
        }
    }
}