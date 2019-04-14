using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;

namespace StudentMajorLeague.Web.Infrastructure
{
    public class StudentMajorLeagueDbContext : DbContext
    {
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<HistoryBlock> HistoryBlocks { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public StudentMajorLeagueDbContext() : base("DbConnectionString")
        {
        }
    }
}