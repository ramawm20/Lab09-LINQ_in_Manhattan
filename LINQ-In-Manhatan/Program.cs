

using Newtonsoft.Json;
using System.Text.Json;
public class Rootobject
{
    public string type { get; set; }
    public Feature[] features { get; set; }
}

public class Feature
{
    public string type { get; set; }
    public Geometry geometry { get; set; }
    public Properties properties { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public float[] coordinates { get; set; }
}

public class Properties
{
    public string zip { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string address { get; set; }
    public string borough { get; set; }
    public string neighborhood { get; set; }
    public string county { get; set; }
}


namespace LINQ_In_Manhatan
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
  _      _____ _   _  ____    _         __  __             _           _   _              
 | |    |_   _| \ | |/ __ \  (_)       |  \/  |           | |         | | | |             
 | |      | | |  \| | |  | |  _ _ __   | \  / | __ _ _ __ | |__   __ _| |_| |_ __ _ _ __  
 | |      | | | . ` | |  | | | | '_ \  | |\/| |/ _` | '_ \| '_ \ / _` | __| __/ _` | '_ \ 
 | |____ _| |_| |\  | |__| | | | | | | | |  | | (_| | | | | | | | (_| | |_| || (_| | | | |
 |______|_____|_| \_|\___\_\ |_|_| |_| |_|  |_|\__,_|_| |_|_| |_|\__,_|\__|\__\__,_|_| |_|
                                                                                          
                                                                                          
");
            string line = new string('-', 100);

            //Read from Json File
            var text = File.ReadAllText(@"./data.json");

            //Convert from string to object
            Rootobject rootObject = JsonConvert.DeserializeObject<Rootobject>(text);

            Feature[] features = rootObject.features;

            //Get all the neighborhoods in the dataList
            var list = from feature in features
                       select feature.properties.neighborhood;

            //Print the outputs
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);

            Console.WriteLine("All the neighborhoods in the dataList");
            Console.WriteLine(line);

            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (var feature in list)
            {
                Console.WriteLine(feature);
            }
            Console.WriteLine(line);

            //Get all neighborhoods in the dataList without the null ones
            var list2 = from n in list
                        where n != ""
                        select n;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.WriteLine("All neighborhoods without the ones without names ");
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var feature in list2)
            {
                Console.WriteLine(feature);
            }


            //Get all neighborhoods in the dataList without the null ones and without dublicates
            var list3 = list2.Distinct();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.WriteLine("All neighborhoods in the dataList without the null ones and without dublicates");
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var feature in list3)
            {
                Console.WriteLine(feature);
            }

            //All without null and dublicates in another way
            var list4 = (from feature in features
                         where feature.properties.neighborhood != ""
                         select feature.properties.neighborhood).Distinct();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.WriteLine("All neighborhoods without null and dublicates in another way");
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var feature in list3)
            {
                Console.WriteLine(feature);
            }

            //Choose one of the previous and do it with LINQ
            var list5 = list.Where(e => e != "").ToList();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.WriteLine("All neighborhoods without null in another way");
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var feature in list3)
            {
                Console.WriteLine(feature);
            }

        }
    }
}