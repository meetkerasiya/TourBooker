using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Transactions;
using TourBooker;

class AppData
{
    public List<Country> AllCountries { get; private set; }

   // public Dictionary<string,Country> AllCountriesByKey { get; private set; }
    public SortedDictionary<CountryCode,Country> AllCountriesByKey { get; private set; }

    public LinkedList<Country> Itenarybuilder { get; } =new LinkedList<Country>();

     

    public void Initialize(string csvFilePath)
    {
        
        CsvReader csvReader = new CsvReader(csvFilePath);
       //this.AllCountries = csvReader.ReadAllCountries();
        this.AllCountries = (csvReader.ReadAllCountries()).OrderBy(c=>c.Name).ToList();
        //Enumerateusing dictionary
        //var dict = AllCountries.ToDictionary(c => c.Code, StringComparer.OrdinalIgnoreCase);
        var dict = AllCountries.ToDictionary(c => c.Code);
        this.AllCountriesByKey = new SortedDictionary<CountryCode, Country>(dict);
    }
    public void display()
    {
        //foreach(var country in this.AllCountries)
        //{
        //    Console.WriteLine(country);
        //}
        //Console.WriteLine();
        //Console.WriteLine("Using Dictionary");
        foreach (var country in this.AllCountriesByKey)
        {
            Console.WriteLine(country);
        }
    }
    public static void Main(string[] args)
    {
        string csvPath = "C:\\Users\\Meet  Kerasiya\\Downloads\\PopByLargest.csv";
        AppData data = new AppData();
        data.Initialize(csvPath);
        data.display();
        /*
                Console.WriteLine("Enter country code to find country's name");
                string code= Console.ReadLine();
                if (code is not null)
                    code = code.ToUpper();
                //instead , we can equality comparer of dictionary
                data.GetCountryWithCode(code);
        */
        data.Itinerary();
        
    }
    private void Itinerary()
    {
        while (true)
        {
            Console.WriteLine("Please enter a choise from following");
            Console.WriteLine("1 To add country in Itinerary");
            Console.WriteLine("2 To remove country from Itinerary");
            Console.WriteLine("3 To add country before a specific country in Itinerary");
            int choise=-1;
            try
            {
                choise = int.Parse(Console.ReadLine());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Please enter choise correctly");

            }
            switch (choise)
            {
                case 1:
                    Console.WriteLine("Enter a country code to add it in itinerary");
                    string code = Console.ReadLine();
                    if (code is not null)
                    {
                        code = code.ToUpper();
                        bool success = this.AllCountriesByKey.TryGetValue(new CountryCode(code), out Country? result);
                        if (success && result is not null)
                        {
                            this.Itenarybuilder.AddLast(result);
                        }
                        else
                        {
                            Console.WriteLine("No country found with this code");
                        }
                    }
                    break;

                 case 2:
                    Console.WriteLine("Enter a country code to remove it from itinerary");
                    code = Console.ReadLine();
                    if (code is not null)
                    {
                        code = code.ToUpper();
                        bool success = this.AllCountriesByKey.TryGetValue(new CountryCode(code), out Country? result);
                        if (success && result is not null)
                        {
                            this.Itenarybuilder.Remove(result);
                        }
                        else
                        {
                            Console.WriteLine("No country found with this code");
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter a country code which you want to insert");
                    code= Console.ReadLine();
                    Console.WriteLine("Enter a country code which you want to insert before");
                    string code2= Console.ReadLine();
                    if (code is not null && code2 is not null)
                    {
                        code = code.ToUpper();
                        code2= code2.ToUpper();
                        bool success = this.AllCountriesByKey.TryGetValue(new CountryCode(code), out Country? result);
                        bool success2 = this.AllCountriesByKey.TryGetValue(new CountryCode(code2), out Country? result2);
                        if (success && success2 && result is not null)
                        {
                            /*LinkedListNode<Country> current_node = this.Itenarybuilder.First;
                            if( current_node.Value==result2 && current_node == this.Itenarybuilder.Last)
                            {
                                this.Itenarybuilder.AddFirst(result);
                                break;
                            }
                            while(current_node.Value != result2 || current_node != null)
                            {
                                current_node = current_node.Next;
                            }
                            if (current_node.Value == result2)
                            {
                                this.Itenarybuilder.AddBefore(current_node,result);
                            }*/
                            var beforeNode=this.Itenarybuilder.Find(result2);
                            
                            if(beforeNode!= null )
                            {
                                this.Itenarybuilder.AddBefore(beforeNode, new LinkedListNode<Country>(result));
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("No country found with this code");
                        }
                    }
                    break;

            }
            this.displayCurrentItenary();
        }
    }

    private void displayCurrentItenary()
    {
        Console.WriteLine("Currently, Itinerary is:");
        foreach (var country in this.Itenarybuilder)
        {
            Console.WriteLine(country);
        }
    }

    private void GetCountryWithCode(string? code)
    {
        if(code.Length != 3)
            Console.WriteLine("Please enter correct code");
        // Console.WriteLine(CsvReader.countries.Find(c=>c.Code==code));
        //doing using dictionary
        //this method returns a boolean
        this.AllCountriesByKey.TryGetValue(new CountryCode(code), out Country result);
        Console.WriteLine(result);
        if (result is null) Console.WriteLine("Please enter correct code");
    }
}