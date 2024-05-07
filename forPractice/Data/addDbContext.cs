using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace forPractice.Data
{
    public class addDbContex : DbContext
    {
        private readonly IConfiguration _configuration;

        public addDbContex(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnectionString"));
        }


        public DbSet<MemberModel> memberModels { get; set; }
    }
}
