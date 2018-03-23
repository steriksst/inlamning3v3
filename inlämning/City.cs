using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inlämning
{
    class City
    {
        //Average_cost_hotel ej med i kontruktorn eftersom averagecost snarare är en metod, ej aktivt mata in värde. 
        public City(string name, int citizens, int average_income, int tourist_per_year, List<Accommodation> accommodations)
        {
            Name = name;
            Citizens = citizens;
            Average_income = average_income;
            this.tourist_per_year = tourist_per_year;
            Accommodations = accommodations;


            Average_cost_hotel = accommodations.Average(a => a.Price);      //LINQ. Fattar ej "a funktionen". Stina: Jag fattar inte heller det.

            antal_accommodations = accommodations.Count();

        }


        public string Name { get; set; }
        public int Citizens { get; set; }
        public int Average_income { get; set; }
        public int tourist_per_year { get; set; }
        public List<Accommodation> Accommodations { get; } = new List<Accommodation>();
        public double antal_accommodations { get; set; }
        public double Average_cost_hotel { get; set; }

    }
}
//Stina: Bra och enl. krav ! Strukturerat.
