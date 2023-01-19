using System.Diagnostics.Metrics;
using TourBooker;

class AppData
{
    public List<Country> AllCountries { get; private set; }
    public void Initialize(string csvFilePath)
    {
        CsvReader csvReader = new CsvReader(csvFilePath);
        this.AllCountries = csvReader.ReadAllCountries();
    }
    public void display()
    {
        foreach(var country in CsvReader.countries)
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
        if(code is not null)
            code = code.ToUpper();
        data.GetCountryWithCode(code);
    }

    private void GetCountryWithCode(string? code)
    {
        if(code.Length != 3)
            Console.WriteLine("Please enter correct code");
        Console.WriteLine(CsvReader.countries.Find(c=>c.Code==code));
    }
}