using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace inlämning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> citynames = new List<string>
            {
                "Boston", "Amsterdam", "Barcelona"
            };
            List<City> cities = new List<City>();

            //skap en SQL-connection 
            SqlConnection conn = new SqlConnection();

            //Connection sträng = en text man måste skicka in för att få kontakt med databasen. 
            //Klicka properties på databasen och kopiera stringen
            conn.ConnectionString = "Data Source=DESKTOP-J421FR9;Initial Catalog=Inlämning3;Integrated Security=True";


            foreach (var item in citynames)
            {

                //Använda trycatch för att kunna se varför vi inte kommer åt servern ifall problem skulle uppstå
                try
                {
                    conn.Open();
                    SqlCommand myQuery = new SqlCommand("SELECT * FROM " + item + ";", conn);

                    //Skapar en SQL datareader 
                    SqlDataReader myReader = myQuery.ExecuteReader();

                    //Skapar en ny lista för accommodation 
                    List<Accommodation> accommodationsList = new List<Accommodation>();

                    //så länge det finns rader läser vi in dem
                    int Reviews;
                    int Room_id;
                    string Borough;
                    double Bedroom;
                    int Host_id;
                    double Price;
                    string Room_type;
                    string Neighborhood;
                    double Overall_satisfaction;
                    double Latitude;
                    double Longitude;
                    string Last_modified;
                    int Minstay;
                    bool Test_minstay;
                    int Accommodates;


                    while (myReader.Read())
                    {
                        Reviews = (int)myReader["reviews"];                            //Genom att sätta (int) säger vi till C# att värdet som ska in från SQL är en int!
                        Room_id = (int)myReader["Room_id"];
                        if (myReader["borough"] is String)
                        {
                            Borough = (string)myReader["borough"];
                        }
                        else
                        {
                            Borough = "";
                        }
                        Host_id = (int)myReader["Host_id"];
                        Neighborhood = myReader["Neighborhood"].ToString();
                        Room_type = myReader["room_type"].ToString();
                        Latitude = Convert.ToDouble(myReader["latitude"]);
                        Longitude = Convert.ToDouble(myReader["longitude"]);
                        Bedroom = Convert.ToDouble(myReader["bedrooms"]);
                        Overall_satisfaction = Convert.ToDouble(myReader["overall_satisfaction"]);
                        Price = Convert.ToDouble(myReader["price"]);
                        Accommodates = (int)myReader["accommodates"];
                        Test_minstay = int.TryParse(Convert.ToString(myReader["minstay"]), out Minstay);
                        if (Test_minstay == false)
                        {
                            Minstay = 0;
                        }
                        else
                        {
                            Minstay = (int)myReader["minstay"];
                        }
                        Last_modified = myReader["last_modified"].ToString();


                        //Console.WriteLine(Minstay);

                        Accommodation accommodation = new Accommodation(
                        Room_id,
                        Host_id,
                        Room_type,
                        Borough,
                        Neighborhood,
                        Reviews,
                        Overall_satisfaction,
                        Accommodates,
                        Bedroom,
                        Price,
                        Minstay,
                        Latitude,
                        Longitude,
                        Last_modified);

                        accommodationsList.Add(accommodation);

                    }
                    //Lägga till Accommodations i Cityobjekt
                    City city = new City(item, 0, 0, 0, accommodationsList);
                    cities.Add(city);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }
                finally
                {
                    conn.Close();
                }
            }

            //FÖR ATT KUNNA PLOTTA I CHARTEN 
            // I En foreach-sats har jag lagt in Cases för att kunna separa datat för varje stad.
            foreach (City c in cities)  // För varje stad i listan city
            {
                switch (c.Name)  //För att kunna ta fram datat för varje stad 
                {
                    case "Boston":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Room_type == "Private room"))  //För varje rum i listan för "Accommodation" + villkor = endast private rooms 
                        {
                            Boston.Series["Price"].Points.AddY(ac.Price);
                        };
                        break;
                    case "Barcelona":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Room_type == "Private room"))
                        {
                            Barcelona.Series["Price"].Points.AddY(ac.Price);
                        };
                        break;

                    case "Amsterdam":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Room_type == "Private room"))
                        {
                            Amsterdam.Series["Price"].Points.AddY(ac.Price);
                        };
                        break;
                    default:
                        break;
                }
            }

            foreach (City c in cities)  
            {
                switch (c.Name)  
                {
                    case "Boston":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Overall_satisfaction < 4.5))
                        {
                            Boston2.Series["Overall Satesfaction"].Points.AddXY(ac.Price, ac.Overall_satisfaction);
                        };
                        break;
                    case "Barcelona":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Overall_satisfaction < 4.5))
                        {
                            Barcelona2.Series["Overall Satesfaction"].Points.AddXY(ac.Price, ac.Overall_satisfaction);
                        };
                        break;

                    case "Amsterdam":
                        foreach (Accommodation ac in c.Accommodations.Where(y => y.Overall_satisfaction < 4.5))
                        {
                            Amsterdam2.Series["Overall Satesfaction"].Points.AddXY(ac.Price, ac.Overall_satisfaction);
                        };
                        break;
                    default:
                        break;
                }
            }

            //UPPGIFT 1, plot för Boston, Barcelona & Amsterdam
        
            Boston.Series["Price"].ChartType = SeriesChartType.Column;
            Boston.Titles.Add("Price spread on Private Rooms in Boston");
            Boston.ChartAreas[0].AxisY.Title = ("Price");
            Boston.ChartAreas[0].AxisX.Title = ("Private Room");

            Barcelona.Series["Price"].ChartType = SeriesChartType.Column;
            Barcelona.Titles.Add("Price spread on Private Rooms in Barcelona");
            Barcelona.ChartAreas[0].AxisY.Title = ("Price");
            Barcelona.ChartAreas[0].AxisX.Title = ("Private Room");

            Amsterdam.Series["Price"].ChartType = SeriesChartType.Column;
            Amsterdam.Titles.Add("Price spread on Private Rooms in Amsterdam");
            Amsterdam.ChartAreas[0].AxisY.Title = ("Price");
            Amsterdam.ChartAreas[0].AxisX.Title = ("Private Room");

            //UPPGIFT2, plot för Boston, Barcelona & Amsterdam 
            Boston2.Series["Overall Satesfaction"].ChartType = SeriesChartType.Point;
            Boston2.Titles.Add("Price vs Overall satesfaction in Boston");
            Boston2.ChartAreas[0].AxisX.Title = ("Price");
            Boston2.ChartAreas[0].AxisY.Title = ("Overall Satesfaction");

            Barcelona2.Series["Overall Satesfaction"].ChartType = SeriesChartType.Point;
            Barcelona2.Titles.Add("Price vs Overall satesfaction in Barcelona");
            Barcelona2.ChartAreas[0].AxisX.Title = ("Price");
            Barcelona2.ChartAreas[0].AxisY.Title = ("Overall Satesfaction");

            Amsterdam2.Series["Overall Satesfaction"].ChartType = SeriesChartType.Point;
            Amsterdam2.Titles.Add("Price vs Overall satesfaction in Amsterdam");
            Amsterdam2.ChartAreas[0].AxisX.Title = ("Price");
            Amsterdam2.ChartAreas[0].AxisY.Title = ("Overall Satesfaction");
        }


        //Kom med för att jag klickade på charten, kan ej ta bort då påverkas koden av någon anledning. 
        private void Boston2_Click(object sender, EventArgs e)
        {

        }
    }
}
