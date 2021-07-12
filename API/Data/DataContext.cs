using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    // we can add db set for the photos but we are going to add the photos to individual user photo collection we are not going to getting photos independently
    public DbSet<AppUser>  Users  { get; set; }

  }
} 