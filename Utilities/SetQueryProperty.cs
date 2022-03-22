/*namespace Utilities;

public class SetQueryProperty
{
    public static IEnumerable<Song> SetQuery(String property, IEnumerable<Song> distinctItems, int songYear)
    {
        var hold = property.ToLower();
        from song in distinctItems
        
        if (hold == "name")
        {
            hold = song.Name;
        }
        if (hold == "artist")
        {
            hold = song.Artist;
        }
        if (hold == "genre")
        {
            hold = song.Genre;
        }
        if (hold == "times played")
        {
            hold = song.Plays;
        }
        
            
            orderby hold
            where song.Year == new DateOnly(songYear,1,1)
            select song;
            
        return song;
    }
}*/