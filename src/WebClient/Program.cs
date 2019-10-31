using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

// TODO: numarul de locuinte autorizate in fiecare luna/semestru (Beniamin Petrovai)
// Ar merge si o piramida a varstelor implementata. (review Google Play 21.03.2019)

namespace WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
