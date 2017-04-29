using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

// TODO: Ar fi interesant o statistica pe tot anul, mai ales cele care s-au incheiat. Nu doar pe luni. [de la Sabin Uivarosan]
// TODO: Acum daca faci si totalul/media anuala cand apas la Clasamente pe an ar fi si mai bine ... sa compari situatii lunare e cam aiurea ... 
//       as sugera cand dai click pe un an sa faca media/totalul pe an, iar cand apesi si pe o luna sa arate situatia pe luna respectiva. Evident la salarii sa faca media, la turisti sa faca totalul, etc. [utopium / SSC]

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
