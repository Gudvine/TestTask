using Microsoft.EntityFrameworkCore;
using TestTask.Data.Models;

namespace TestTask.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public virtual DbSet<FileInstance> Files { get; set; }
    }
}
