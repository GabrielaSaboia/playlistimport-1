// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Utilities;
using static Utilities.ConsoleRead;//imported method from utilities
using static Utilities.ConsoleWrite;//imported method from utilities
using static Utilities.OrderByProperty;

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
if (filePath != "") //filePath check to assing a user defined file path
{
    absoluteFilePath = filePath;
}
using (var reader = new StreamReader(absoluteFilePath))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<SongMap>();
    WriteToConsole("Reading the CSV File\r");
    records = csv.GetRecords<Song>().ToList();

}
WriteToConsole($"Record Count = {records.Count}\r");
WriteToConsole("_____________________________\r");

//function call to ask user to define how to organize their list
IEnumerable<Song> songQuery = QueryByProperty(records, songYear);

var songQueryResults = songQuery.ToList();
var songCountCount = songQueryResults.Count.ToString();
WriteToConsole(songCountCount);
foreach (Song song in songQueryResults)
{
    Console.WriteLine("{0},{1}, {2}, {3}",song.Name,song.Artist, song.Genre, song.Plays);
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





