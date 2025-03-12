using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class taskContext : DbContext
    {

        public taskContext(DbContextOptions<taskContext> options) : base(options) { }



        public DbSet<problems1> problems1 { get; set; }

        public DbSet<ProblemInfo> ProblemInfo { get; set; }

        public DbSet<Submission> Submission { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<RankViewModel> RankViewModel { get; set; }

        public DbSet<UserImg> UserImg { get; set; }
        public DbSet<Profile> Profile
        {
            get; set;

        }
        


    }
}
