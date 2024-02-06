using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;

namespace NetClubApi.ClubModule
{

    interface IClubDataAccess
    {
        public  Task<List<ClubRegistration>> getCreatedClub(string id);
        public  Task<Club> getClubDetails(int club_id);
    }

    public class ClubDataAccess : IClubDataAccess
    {
        public readonly NetClubDbContext _netClubDbContext;
        
        public ClubDataAccess(NetClubDbContext netClubDbContext) {
            _netClubDbContext = netClubDbContext;
            
            
        }

        //return the list of clubs created by the user
        public async Task<List<ClubRegistration>> getCreatedClub(string id)
        {
             List<ClubRegistration> clubs = new();
            try
            {

                //get the list of clubs created by the user using user id and role
                 clubs = await _netClubDbContext.club_registrations.Where(clubs => clubs.user_id.ToString() == id && clubs.isadmin).ToListAsync();

                
                return clubs;
                

                    
            }
            catch(Exception)
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
            catch(Exception)
            {
                throw;
            }
        }
    }
}
