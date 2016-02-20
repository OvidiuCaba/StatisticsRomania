using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal static class CountiesSeeder
    {
        internal static List<County> GetData()
        {
            var items = new List<County>
                               {
                                   new County {Id = 1, Name = "Alba"},
                                   new County {Id = 2, Name = "Arad"},
                                   new County {Id = 3, Name = "Arges"},
                                   new County {Id = 4, Name = "Bacau"},
                                   new County {Id = 5, Name = "Bihor"},
                                   new County {Id = 6, Name = "Bistrita Nasaud"},
                                   new County {Id = 7, Name = "Botosani"},
                                   new County {Id = 8, Name = "Brasov"},
                                   new County {Id = 9, Name = "Braila"},
                                   new County {Id = 10, Name = "Buzau"},
                                   new County {Id = 11, Name = "Caras-Severin"},
                                   new County {Id = 12, Name = "Calarasi"},
                                   new County {Id = 13, Name = "Cluj"},
                                   new County {Id = 14, Name = "Constanta"},
                                   new County {Id = 15, Name = "Covasna"},
                                   new County {Id = 16, Name = "Dambovita"},
                                   new County {Id = 17, Name = "Dolj"},
                                   new County {Id = 18, Name = "Galati"},
                                   new County {Id = 19, Name = "Giurgiu"},
                                   new County {Id = 20, Name = "Gorj"},
                                   new County {Id = 21, Name = "Harghita"},
                                   new County {Id = 22, Name = "Hunedoara"},
                                   new County {Id = 23, Name = "Ialomita"},
                                   new County {Id = 24, Name = "Iasi"},
                                   new County {Id = 25, Name = "Ilfov"},
                                   new County {Id = 26, Name = "Maramures"},
                                   new County {Id = 27, Name = "Mehedinti"},
                                   new County {Id = 28, Name = "Mures"},
                                   new County {Id = 29, Name = "Neamt"},
                                   new County {Id = 30, Name = "Olt"},
                                   new County {Id = 31, Name = "Prahova"},
                                   new County {Id = 32, Name = "Satu Mare"},
                                   new County {Id = 33, Name = "Salaj"},
                                   new County {Id = 34, Name = "Sibiu"},
                                   new County {Id = 35, Name = "Suceava"},
                                   new County {Id = 36, Name = "Teleorman"},
                                   new County {Id = 37, Name = "Timis"},
                                   new County {Id = 38, Name = "Tulcea"},
                                   new County {Id = 39, Name = "Vaslui"},
                                   new County {Id = 40, Name = "Valcea"},
                                   new County {Id = 41, Name = "Vrancea"},
                                   new County {Id = 42, Name = "Bucuresti"},
                               };
            return items;
        }
    }
}