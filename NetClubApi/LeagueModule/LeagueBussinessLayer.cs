using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.LeagueModule
{
    public interface ILeagueBussinessLayer
    {
        public Task<List<LeagueResponse>> GetClubLeagues(int club_id);
        public Task<int?> GetLeagueTeams(int league_id);
        public  Task<List<LeagueResponse>> ConvertToLeagueResponse(List<League> leagues);
        public Task<string> RegisterLeague(LeagueRegistration league);
        public Task<List<MyLeagues>> GetMyLeagues(int user_id);
    }

    
    public class LeagueBussinessLayer : ILeagueBussinessLayer
    {
        private readonly ILeagueDataAccess _dataAccess;

        public LeagueBussinessLayer(ILeagueDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        public async Task<List<LeagueResponse>> GetClubLeagues(int club_id)
        {
            List<League> leagues = await _dataAccess.GetLeagues(club_id);
            List<LeagueResponse> leagueResponses = await ConvertToLeagueResponse(leagues);
            return leagueResponses;
        }

        public async Task<int?> GetLeagueTeams(int league_id)
        {
            //get the number of teames in the given league
            int? NumberOfTeams = await _dataAccess.getLeagueTeams(league_id);
            return NumberOfTeams;

        }

        public  async Task<List<LeagueResponse>> ConvertToLeagueResponse(List<League> leagues)
        {
            List<LeagueResponse> responses = new();
            foreach(League league in leagues)
            {
                LeagueResponse leagueResponse = new();
                leagueResponse.LeagueName = league.name;
                leagueResponse.StartDate = league.start_date;
                leagueResponse.EndDate = league.end_date;
                leagueResponse.Teams = league.number_of_teams;
                leagueResponse.Matches = await GetLeagueTeams(league.Id);
                responses.Add(leagueResponse);
            }
            return responses;

        }

        public async Task<string> RegisterLeague(LeagueRegistration league)
        {
            try
            {
                
                return await _dataAccess.RegisterLeague(league);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<MyLeagues>> GetMyLeagues(int user_id)
        {
            List<LeagueRegistration> registeredLeagues = await _dataAccess.GetMyLeagues(user_id);
            List<MyLeagues> myLeagues = new();
            foreach(LeagueRegistration league in registeredLeagues)
            {
                MyLeagues myLeague = new();
                myLeague.LeagueName = GetLeagueName(await _dataAccess.GetLeague(league.league_id));
                myLeague.ClubName = GetClubName(await _dataAccess.GetClub(league.club_id));
                myLeague.Win = 0;
                myLeague.Points = 0;
                myLeague.Rank = 12;
                myLeague.Loss = 6;
                myLeague.Rating = 0;
                myLeague.WinPercentage = 20;
                myLeagues.Add(myLeague);

            }
            return myLeagues;
        }

        private string GetClubName(Club club)
        {
            return club.club_name;
        }

        private string GetLeagueName(League league)
        {
            return league.name;
        }
    }
}
