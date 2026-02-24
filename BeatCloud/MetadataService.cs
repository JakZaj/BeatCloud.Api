using TagLib;
public class MetadataService
{

    public void GetMusicMetadate(string filePath)
    {
        var tfile = TagLib.File.Create(filePath);

        string title = tfile.Tag.Title;
        string artist = tfile.Tag.FirstAlbumArtist;
        uint year = tfile.Tag.Year;

        Console.WriteLine($"Utwór: {title} | Wykonawaca: {artist} | Rok: {year}");
    }

}
