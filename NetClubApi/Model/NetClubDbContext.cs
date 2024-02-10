using Microsoft.EntityFrameworkCore;

namespace NetClubApi.Model
{
    public class NetClubDbContext : DbContext
    {

        public NetClubDbContext(DbContextOptions<NetClubDbContext> options):base(options)
        {

        }


        public DbSet<UserModel> User_detail { get; set; } 
        public DbSet<Club> club { get; set; }
        public DbSet<ClubRegistration> club_registration { get; set; }

        

    }
}
