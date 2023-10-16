using Microsoft.EntityFrameworkCore;
using TestTask.Data.Models;

namespace TestTask.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public virtual DbSet<FileInstance> DocFiles { get; set; }
        public virtual DbSet<WeatherArchive> WeatherArchives { get; set; }
        public virtual DbSet<WeatherArchiveFile> WeatherArchiveFiles { get; set; }
        public virtual DbSet<ArchiveSheet> ArchiveSheets { get; set; }
    }
}
