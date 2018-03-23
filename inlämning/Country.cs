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

    /* Koden i filerna Country.cs, City.cs och Accomodation.cs är enkel men bra. Vissa funktioner i filen City.cs verkar vara krånglig men annars är allt ok. Koden är snyggt skriven med bra struktur i alla tre filer.
Inget märkvärdigt har lämnats utanför i dessa tre filer då det som är viktigt och nödvändigt är med. Hela uppgiften allmänt ser bra ut då det finns grafer som visualiserar informationen via koden. Kraven på innehållet
är uppfyllt då arbetet innehåller klasser, medlemsvariabler samt medlemsvariabler enligt del 1, 6 st plottar finns med namn på x och y axlar. Vart datamängden kommer från visas även i plottarna. Även de tekniska kraven ser bra ut.
Alla klasser är i separata filer samt uppladdad på personlig github. Datapunkterna läses in som objekt i respektive lista i City objekten och plottarna ritas upp genom hämtad data från dessa objekt där alla accommodation-objekt finns lagrade i en lista.
Jag tycker arbetet är bra gjort med fin struktur på koden och bra resultat i slutändan. För att vara en ganska stor samt svår uppgift ser jag inget Momina kan göra bättre i sitt arbete. */
}
//Stina: Håller med ovanstående, allt verkar bra och enl. krav.
