using Hypernodes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperNodes.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<MediaObject> MediaObjects { get; set; }
}