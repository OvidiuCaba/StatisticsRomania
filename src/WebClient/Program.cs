using Microsoft.AspNetCore.Hosting;
using System.IO;

// TODO: numarul de locuinte autorizate in fiecare luna/semestru (Beniamin Petrovai)
// Ar merge si o piramida a varstelor implementata. (review Google Play 21.03.2019)

namespace WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
