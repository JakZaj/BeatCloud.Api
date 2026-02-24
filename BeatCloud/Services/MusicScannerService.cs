using BeatCloud.Api.Data;
using BeatCloud.Api.Models;
using TagLib;


namespace BeatCloud.Api.Services;

public class MusicScannerService
{
    private readonly ApplicationDbContext _context;

    public MusicScannerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ScanFolder(string folderPath) 
    { 
        if (!Directory.Exists(folderPath)) return;

        var files = Directory.GetFiles(folderPath, "*.mp3", SearchOption.AllDirectories);

        foreach (var filePath in files)
        {
            try
            {
                using var tfile = TagLib.File.Create(filePath);

                var song = new Song
                {
                    Title = tfile.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
                    Artist = tfile.Tag.FirstAlbumArtist ?? tfile.Tag.FirstPerformer ?? "Unknown Artist",
                    Year = (int)tfile.Tag.Year != 0 ? (int)tfile.Tag.Year : null,
                    FilePath = filePath

                };

                if (!_context.Songs.Any(s => s.FilePath == filePath))
                {
                    _context.Songs.Add(song);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {filePath} error: {ex.Message}");
            }
        }

        _context.SaveChanges();
    }
}
