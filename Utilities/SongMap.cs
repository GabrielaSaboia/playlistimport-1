using CsvHelper.Configuration;

namespace Utilities;

public class SongMap : ClassMap<Song>
{
    public SongMap()
    { 
        Map(m => m.Name);
        Map(m => m.Artist);
        Map(m => m.Composer);
        Map(m => m.Genre);
        Map(m => m.Year).TypeConverter<CustomDateYearConverter>();
        Map(m => m.Plays).TypeConverter<CustomIntConverter>();}
   
}