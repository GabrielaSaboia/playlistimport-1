using static Utilities.ConsoleWrite;
namespace Utilities;

public class DisplayQuery
{
    public static void QueryList(IEnumerable<Song> songQuery)
    {
        var songQueryResults = songQuery.ToList();
        var songCountCount = songQueryResults.Count.ToString();
        WriteToConsole(songCountCount);
        foreach (Song song in songQueryResults)
        {
            Console.WriteLine("{0},{1}, {2}, {3}",song.Name,song.Artist, song.Genre, song.Plays);
        }
    }
}