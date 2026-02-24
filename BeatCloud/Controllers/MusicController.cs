using Microsoft.AspNetCore.Mvc;
using BeatCloud.Api.Services;
using Microsoft.AspNetCore.StaticFiles;
using BeatCloud.Api.Data;

namespace BeatCloud.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MusicController : ControllerBase
{
    private readonly MusicScannerService _scannerService;
    private readonly ApplicationDbContext _context;

    public MusicController(MusicScannerService scannerService, ApplicationDbContext context)
    {
        _scannerService = scannerService;
        _context = context;
    }

    [HttpPost("scan")]
    public IActionResult ScanMusic([FromQuery] string folderPath) {
        if (string.IsNullOrWhiteSpace(folderPath))
            return BadRequest("Path to folder is empty");

        try
        {
            _scannerService.ScanFolder(folderPath);
            return Ok("Scan end successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> StreamMusic(int id)
    {
        var song = await _context.Songs.FindAsync(id);

        if (song == null || !System.IO.File.Exists(song.FilePath))
            return NotFound("Song file not found");

        var stream = new FileStream(song.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);

        new FileExtensionContentTypeProvider().TryGetContentType(song.FilePath, out var contentType);

        return File(stream, contentType ?? "audio/mpeg", enableRangeProcessing: true);
    }
}

