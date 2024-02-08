using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;

namespace NetClubApi.ClubModule
{

    public interface IClubDataAccess
    {
        public Task<List<ClubRegistration>> getCreatedClub(int id);
        public Task<Club> getClubDetails(int club_id);
        public Task<string> CreateClub(Club club,int id);
    }

    public class ClubDataAccess : IClubDataAccess
    {
        private readonly NetClubDbContext _netClubDbContext;
        
        public ClubDataAccess(NetClubDbContext netClubDbContext) {
            _netClubDbContext = netClubDbContext;


        }

        //return the list of clubs created by the user
        public async Task<List<ClubRegistration>> getCreatedClub(int id)
        {
            List<ClubRegistration> clubs = new();
            try
            {

                //get the list of clubs created by the user using user id and role
                clubs = await _netClubDbContext.club_registration.Where(clubs => clubs.user_id == id && clubs.isadmin).ToListAsync();


                return clubs;



            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<Club> getClubDetails(int club_id)
        {
            try
            {
                Console.WriteLine(club_id + 1);
                // Retrieve a single club entity with the specified club_id
                var clubs = await _netClubDbContext.club.FirstOrDefaultAsync(user => (
                          user.Id == club_id)
                    );
                Console.WriteLine(clubs);
                return clubs;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> CreateClub(Club club,int userId)
        {

            try
            {
                var registerclub  = await _netClubDbContext.club.AddAsync
(club);
                await _netClubDbContext.SaveChangesAsync();
                ClubRegistration clubRegistration = new();

                clubRegistration.user_id = userId;
                clubRegistration.club_id = registerclub.Entity.Id;
                clubRegistration.registered_date = DateTime.Now;
                clubRegistration.isadmin = true;
                await _netClubDbContext.AddAsync(clubRegistration);
                await _netClubDbContext.SaveChangesAsync();



                return "Club created Successfully";
            }
            catch(Exception)
            {
                throw;
            }


        }
    }
}
