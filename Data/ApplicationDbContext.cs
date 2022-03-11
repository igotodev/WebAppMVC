using Microsoft.EntityFrameworkCore;
using WebAppMVC.Models;

namespace WebAppMVC.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<NoteViewModel> NoteViewModels { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}