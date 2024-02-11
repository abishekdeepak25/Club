using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;
using System.Data.SqlTypes;

namespace NetClubApi.ClubModule
{

    public interface IClubDataAccess
    {
        public Task<List<ClubRegistration>> getCreatedClub(int id);
        public Task<Club> getClubDetails(int club_id);
        
        public Task<string> CreateClub(Club club,int id);
        public Task<List<ClubRegistration>> getRegisteredClub(int id);
        public Task<string> ClubRegistration(Club code,int user_id);
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
            catch   (Exception)
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
                club.registered_date = DateTime.Now;
                club.club_label = await GenerateUniqueLabel();


                var registerclub  = await _netClubDbContext.club.AddAsync
(club);
                await _netClubDbContext.SaveChangesAsync();
                ClubRegistration clubRegistration = new();

                clubRegistration.user_id = userId;
                clubRegistration.club_id = registerclub.Entity.Id;
                
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

        
        public async Task<List<ClubRegistration>> getRegisteredClub(int id)
        {
            List<ClubRegistration> clubs = new();
            try
            {

                
                clubs = await _netClubDbContext.club_registration.Where(clubs => clubs.user_id == id &&  !clubs.isadmin).ToListAsync();


                return clubs;



            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<string> ClubRegistration(Club code,int user_id)
        {
            // get the club id using the code
            var club = await _netClubDbContext.club.FirstOrDefaultAsync(club => club.club_label == code.club_label);

            if (club == default)
                return "club not found";
            var club_id = club.Id;
            if ( await IsAlreadyRegister(club_id,user_id))
                return "already register in this club";
            else
            {
                ClubRegistration clubRegistration = new();
                clubRegistration.user_id = user_id;
                clubRegistration.club_id = club_id;

                clubRegistration.isadmin = false;
                clubRegistration.league_played = 0;

                clubRegistration.join_date = DateTime.Now;
                await _netClubDbContext.club_registration.AddAsync(clubRegistration);
                await _netClubDbContext.SaveChangesAsync();
                return "you registered to the club";
            }
        }

        private async Task<bool> IsAlreadyRegister(int club_id, int user_id)
        {
           var club = await _netClubDbContext.club_registration.FirstOrDefaultAsync(club => club.Id == club_id && club.user_id == user_id);
            if (club == default)
                return false;
            return true;
        }

        private async Task<string> GenerateUniqueLabel()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int labelLength = 6;
            Random random = new Random();
            string randomString;
            do
            {
                randomString = new string(Enumerable.Range(0, labelLength)
                    .Select(_ => characters[random.Next(characters.Length)])
                    .ToArray());
            } while (await IsStringExistsInDatabase(randomString));

            return randomString;
        }

        private async Task<bool> IsStringExistsInDatabase(string randomString)
        {   
            var club = await _netClubDbContext.club.FirstOrDefaultAsync(club => club.club_label==randomString);
            if (club != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
