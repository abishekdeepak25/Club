using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;

namespace NetClubApi.ClubModule
{

    public interface IClubDataAccess
    {
        public Task<List<ClubRegistration>> getCreatedClub(int id);
        public Task<Club> getClubDetails(int club_id);
        public Task<string> CreateClub(Club club,string id);
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
                clubs = await _netClubDbContext.club_registration.Where(clubs => clubs.user_id == id.ToString() && clubs.isadmin).ToListAsync();


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
                // club details
                Club club = await _netClubDbContext.club.FirstOrDefaultAsync(club => club.Id == club_id);
                return club;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> CreateClub(Club club,string userId)
        {

            try
            {
                var registerclub  = await _netClubDbContext.club.AddAsync(club);
                ClubRegistration clubRegistration = new();

                clubRegistration.user_id = userId;
                clubRegistration.club_id = registerclub.Entity.Id.ToString();
                clubRegistration.registered_date = DateTime.Now.ToString();
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
