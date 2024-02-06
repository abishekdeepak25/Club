using NetClubApi.Model;

namespace NetClubApi.ClubModule
{

    interface IClubBussinessLogics
    {
        public Task<List<MyClub>> getMyClubs();
    }
    public class ClubBussinessLogic : IClubBussinessLogics
    {
        private readonly IClubDataAccess _clubDataAccess;
        
        public ClubBussinessLogic (IClubDataAccess clubDataAccess)
        {
            _clubDataAccess = clubDataAccess;
        }

        public async Task<List<MyClub>> getMyClubs()
        {
            //var listOfClubs = new();
            //var listOfMyclubs = 
            //foreach (var club in clubs)
            //{
            //    Club clubDetails = await getClub(club.club_id);
            //    MyClub myClub = new();
            //    myClub.Name = clubDetails.club_name;
            //    myClub.TotalLeagues = clubDetails.total_league;
            //    myClub.ActiveLeagues = clubDetails.active_league;
            //    myClub.Teams = clubDetails.teams;

            //}
        }
    }
}
