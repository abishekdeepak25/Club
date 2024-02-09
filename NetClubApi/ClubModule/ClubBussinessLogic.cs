using NetClubApi.Model;
using Org.BouncyCastle.Utilities;

namespace NetClubApi.ClubModule
{

    public interface IClubBussinessLogics
    {
        public Task<List<MyClub>> getMyClubs(int user_id);
        public Task<List<MyClub>> RegisteredClubs(int user_id);
        public  Task<List<MyClub>> getClubDetails(List<ClubRegistration> clubs);
    }
    public class ClubBussinessLogic : IClubBussinessLogics
    {
        private readonly IClubDataAccess _clubDataAccess;
        
        public ClubBussinessLogic (IClubDataAccess clubDataAccess)
        {
            _clubDataAccess = clubDataAccess;
        }

        public async Task<List<MyClub>> getMyClubs(int user_id)
        {

            try
            {
                // get the list of clubs created by us
                var clubs = await _clubDataAccess.getCreatedClub(user_id);

                //gathering details for the list of clubs
                return await getClubDetails(clubs);
            }
            catch(Exception )
            {
                throw;
            }

            
        }


        //gathering details for the list of clubs
        public async Task<List<MyClub>> getClubDetails(List<ClubRegistration> clubs)
        {
            List<MyClub> listOfClubs = new();
            foreach (var club in clubs)
            {
                if (club != null)
                {

                    Club clubdetails = await _clubDataAccess.getClubDetails(club.club_id);
                    if (clubdetails != null)
                    {
                        MyClub myclub = new();
                        myclub.Name = clubdetails.club_name;
                        myclub.TotalLeagues = clubdetails.total_league;
                        myclub.ActiveLeagues = clubdetails.active_league;
                        myclub.Teams = clubdetails.teams;
                        listOfClubs.Add(myclub);
                    }
                }
            }
            return listOfClubs;
        }
        public async Task<List<MyClub>> RegisteredClubs(int user_id)
        {
            try
            {
                var clubs = await _clubDataAccess.getRegisteredClub(user_id);
                return await  getClubDetails(clubs);
            }
            catch(Exception )
            {
                throw;
            }
            

        }
    }
}
