using Microsoft.EntityFrameworkCore;
using Note.Models; // or your actual namespace

namespace Note.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<NoteModel> Notes { get; set; }
    }
}
