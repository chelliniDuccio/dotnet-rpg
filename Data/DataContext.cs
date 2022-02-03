using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterClassConfiguration> ClassConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "zap", Damage = 20 },
                new Skill { Id = 2, Name = "arrows", Damage = 35 },
                new Skill { Id = 3, Name = "fireball", Damage = 50 }
                );

            modelBuilder.Entity<CharacterClassConfiguration>().HasIndex(c => c.Class).IsUnique();
        }
    }
}
