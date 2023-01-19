using System.Diagnostics.Metrics;
using TourBooker;

class AppData
{
    public List<Country> AllCountries { get; private set; }

   // public Dictionary<string,Country> AllCountriesByKey { get; private set; }
    public SortedDictionary<CountryCode,Country> AllCountriesByKey { get; private set; }

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

        Console.WriteLine("Enter country code to find country's name");
        string code= Console.ReadLine();
        if (code is not null)
            code = code.ToUpper();
        //instead , we can equality comparer of dictionary
        data.GetCountryWithCode(code);
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