using Microsoft.EntityFrameworkCore;
using BeatCloud.Api.Models;

namespace BeatCloud.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Song> Songs { get; set; }
}
