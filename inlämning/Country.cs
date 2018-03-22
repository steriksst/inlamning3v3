using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inlämning
{
    class Country
    {
        public Country(string name, int citizens, int bnp_cap, List<City> cities)
        {
            Name = name;
            Citizens = citizens;
            Bnp_cap = bnp_cap;
            Cities = cities;
        }

        public string Name { get; set; }
        public int Citizens { get; set; }
        public int Bnp_cap { get; set; }
        public List<City> Cities { get; } = new List<City>();

    }
}
