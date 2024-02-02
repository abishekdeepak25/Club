using Microsoft.EntityFrameworkCore;

namespace NetClubApi.Model
{
    public class NetClubDbContext : DbContext
    {

        public NetClubDbContext(DbContextOptions<NetClubDbContext> options):base(options)
        {

        }


        public DbSet<User> User_detail { get; set; } 
    }
}
