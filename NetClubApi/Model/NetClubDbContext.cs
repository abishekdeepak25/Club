using Microsoft.EntityFrameworkCore;

namespace NetClubApi.Model
{
    public class NetClubDbContext : DbContext
    {

        public NetClubDbContext(DbContextOptions<NetClubDbContext> options):base(options)
        {

        }


        public DbSet<User> user_detail { get; set; }
    }
}
