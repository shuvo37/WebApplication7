using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class taskContext : DbContext
    {

        public taskContext(DbContextOptions<taskContext> options) : base(options) { }

        public DbSet<taskList> taskList { get; set; }

        public DbSet<problems1> problems1 { get; set; }

        public DbSet<ProblemInfo> ProblemInfo { get; set; }

        public DbSet<Submission> Submission { get; set; }
       public DbSet<User> User { get; set; }

    }
}
