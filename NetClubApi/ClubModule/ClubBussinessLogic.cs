using NetClubApi.Model;
using Org.BouncyCastle.Utilities;

namespace NetClubApi.ClubModule
{

    public interface IClubBussinessLogics
    {
        public Task<List<MyClub>> getMyClubs(int user_id);
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
            List<MyClub> listOfClubs = new();

            // get the list of clubs created by us
            var clubs = await  _clubDataAccess.getCreatedClub(user_id);

            //gathering details for the list of clubs
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
    }
}
