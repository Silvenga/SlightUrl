namespace SlightUrl.Service.SelfHost
{
    using System;

    using Microsoft.Owin.Hosting;

    public class Program
    {
        public static void Main()
        {
            const string baseAddress = "http://localhost:9900/";

            Console.WriteLine("Starting OWIN.");

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine($"Ready on {baseAddress}");
                Console.ReadLine();
                Console.WriteLine("Disposing...");
            }
        }
    }
}