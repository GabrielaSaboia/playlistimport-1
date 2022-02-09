namespace Utilities;

//created Song.cs file with the function from the main program.
//Now main only has "song" instances, no class declaration of song.
public class Song
{
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Composer { get; set; }
    public string Genre { get; set; }
    public DateOnly Year { get; set; }
    public int Plays { get; set; }
}