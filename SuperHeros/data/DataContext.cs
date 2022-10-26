global using Microsoft.EntityFrameworkCore;

namespace SuperHeros.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
