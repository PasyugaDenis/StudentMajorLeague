using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;

namespace StudentMajorLeague.Web.Infrastructure
{
    public class SMLDbContext : DbContext
    {
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<HistoryBlock> HistoryBlocks { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public SMLDbContext() : base("DbConnectionString")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                .Map(m =>
                {
                    m.Properties(d => new { d.Id, d.Email, d.Password, d.RegistrationDate, d.RoleId });
                    m.ToTable("Users");
                })
                .Map(m =>
                {
                    m.Properties(d => new { d.Name, d.Surname, d.Birthday, d.Education, d.Phone, d.City, d.LeagueId });
                    m.ToTable("Profiles");
                })
                .Map(m =>
                {
                    m.Properties(d => new { d.Weight, d.Height });
                    m.ToTable("Parameters");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Results)
                .WithRequired(r => r.User);

            modelBuilder.Entity<User>()
                .HasRequired(u => u.Chain)
                .WithRequiredPrincipal(c => c.User);

            //Role
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithRequired(u => u.Role);
            
            //Result

            //League
            modelBuilder.Entity<League>()
                .HasMany(l => l.Users)
                .WithRequired(u => u.League);

            //HistoryBlock

            //Competition
            modelBuilder.Entity<Competition>()
                .HasMany(c => c.Results)
                .WithRequired(r => r.Competition);

            //Chain
            modelBuilder.Entity<Chain>()
                .HasMany(c => c.HistoryBlocks)
                .WithRequired(hb => hb.Chain);

            base.OnModelCreating(modelBuilder);
        }
    }
}