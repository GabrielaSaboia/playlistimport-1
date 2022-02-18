namespace Utilities;

public class SetQueryProperty
{
    public static IEnumerable<Song> SetQuery(String property, IEnumerable<Song> distinctItems, int songYear)
    {
        var hold = property.ToLower();
        if (hold == "name")
        {
            hold = "Name";
        }
        if (hold == "artist")
        {
            hold = "Artist";
        }
        if (hold == "genre")
        {
            hold = "Genre";
        }
        if (hold == "times played")
        {
            hold = "Plays";
        }
        
            from song in distinctItems
            orderby song.hold
            where song.Year == new DateOnly(songYear,1,1)
            select song;
            
        return song;
    }
}