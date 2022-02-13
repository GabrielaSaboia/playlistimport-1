// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Utilities;
using static Utilities.ConsoleRead;//imported method from utilities
using static Utilities.ConsoleWrite;//imported method from utilities

//you will need to run "dotnet add package CsvHelper" inside the consoleApp2 Project folder or create the project
//if you are doing this from scratch or you can create the project with the solution by checking that
//box when you create it and just add it in the project solution directory
//put the path to the file you want to import
WriteToConsole("Enter The Absolute File Path for the playlist\r");//refactored the writeline method
var absoluteFilePath = "";
var filePath = ReadConsole();//refactored readline method
if (filePath == "")
{
    absoluteFilePath = DefaultPath.FilePath();//importing filepath from utilities
}
WriteToConsole("Enter The year\r"); //utilities function call
var readYear = ReadConsole();//ConsoleWrite utilities function call 
var songYear = 2015;
if (readYear != String.Empty)
{
    songYear = int.Parse(readYear);
    WriteToConsole(songYear.ToString()); //reference to the ConsoleWrite class in the utilities namespace and function call
}
//here is creating a new list type using a function
var records = CreateNewListOfType<Song>();

List<T> CreateNewListOfType<T>()
{
    List<T> records = new List<T>();
    return records;
}
Console.WriteLine(filePath);
IEnumerable<Song> songs = new List<Song>();
if (filePath != "")
{
    absoluteFilePath = filePath;
}
using (var reader = new StreamReader(absoluteFilePath))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<SongMap>();
    Console.WriteLine("Reading the CSV File\r");
    records = csv.GetRecords<Song>().ToList();

}
Console.WriteLine($"Record Count = {records.Count}\r");
Console.WriteLine("_____________________________\r");
//removes duplicates
var distinctItems = records.GroupBy(x => x.Name).Select(y => y.First());
//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
IEnumerable<Song> songQuery =
    from song in distinctItems
    orderby song.Plays
    where song.Year == new DateOnly(songYear,1,1)
    select song;

var songQueryResults = songQuery.ToList();
var songCountCount = songQueryResults.Count.ToString();
Console.WriteLine(songCountCount);
foreach (Song song in songQueryResults)
{
    Console.WriteLine("{0},{1}, {2}",song.Name,song.Artist, song.Genre);
}

using (var writer = new StreamWriter("./Output.csv"))
using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csvWriter.WriteRecords(songQueryResults);
}
WriteToConsole("\nDone");


/*







foreach (Song song in songQuery)
{
    Console.WriteLine("{0},{1}, {2}",song.Name,song.Artist, song.Genre);
}
Console.WriteLine($"Record Count = {songQuery.Count()}\r");

using (var writer = new StreamWriter("./Output.csv"))
using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    Console.WriteLine($"Record Count = {songQuery.Count()}\r");
    csvWriter.WriteRecords(songQuery);
}
*/
public class SongMap : ClassMap<Song>
{
    public SongMap()
    {
        Map(m => m.Name);
        Map(m => m.Artist);
        Map(m => m.Composer);
        Map(m => m.Genre);
        Map(m => m.Year).TypeConverter<CustomDateYearConverter>();
        Map(m => m.Plays).TypeConverter<CustomIntConverter>();
    }
}

public class CustomIntConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text != "")
        {
            return int.Parse(text);
        }
        else
        {
            return 0;
        }
    }
}
//converting for year
public class CustomDateYearConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text != "")
        {
            var year = int.Parse(text);

            var date = new DateOnly(year, 1, 1);
            return date;
        }
        else
        {
            var date = new DateOnly(1, 1, 1);
            return date;
        }
    }
}

