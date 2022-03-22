using static Utilities.ConsoleWrite;
using static Utilities.ConsoleRead;
namespace Utilities;


public class OrderByProperty
{
    public static IEnumerable<Song> QueryByProperty(List<Song> records, int songYear)
    {
        
        WriteToConsole("Choose how you want to sort your list: " +
                       "\nSong Name" +
                       "\nArtist" +
                       "\nGenre" +
                       "\nTimes Played");
        var choice = ReadConsole();
        //removes duplicates
        var distinctItems = records.GroupBy(x => x.Name).Select(y => y.First());
        var property = choice.ToLower();
        if (property != "")
        {
            if (property == "name")
            {
                IEnumerable<Song> songQuery = 
                    from song in distinctItems
                    orderby song.Name 
                    where song.Year == new DateOnly(songYear,1,1)
                    select song;
                return songQuery;
            }
            if (property == "artist")
            {
                IEnumerable<Song> songQuery = 
                    from song in distinctItems
                    orderby song.Artist
                    where song.Year == new DateOnly(songYear,1,1)
                    select song;
                return songQuery;
            }
            if (property == "genre")
            {
                IEnumerable<Song> songQuery = 
                    from song in distinctItems
                    orderby song.Genre 
                    where song.Year == new DateOnly(songYear,1,1)
                    select song;
                return songQuery;
            }
            if (property == "times played")
            {
                IEnumerable<Song> songQuery = 
                    from song in distinctItems
                    orderby song.Plays 
                    where song.Year == new DateOnly(songYear,1,1)
                    select song;
                return songQuery;
            }
            else
            {
                WriteToConsole("Try again");
            }
        }
        else
        {
            IEnumerable<Song> songQuery = 
                from song in distinctItems
                orderby song.Plays 
                where song.Year == new DateOnly(songYear,1,1)
                select song;
            return songQuery;
        }

        return null;
    }
}