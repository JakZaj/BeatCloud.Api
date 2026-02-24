using System.ComponentModel.DataAnnotations;

namespace BeatCloud.Api.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Artist { get; set; } = "Unknown";

        public int? Year { get; set; }

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
