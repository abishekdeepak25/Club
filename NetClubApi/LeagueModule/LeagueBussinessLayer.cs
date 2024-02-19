using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.LeagueModule
{
    public interface ILeagueBussinessLayer
    {
        public Task<List<LeagueResponse>> GetClubLeagues(int club_id);
        public Task<int?> GetLeagueMatches(int league_id);
        public  Task<List<LeagueResponse>> ConvertToLeagueResponse(List<League> leagues);
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

        public async Task<int?> GetLeagueMatches(int league_id)
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
                leagueResponse.Matches = await GetLeagueMatches(league.Id);
                responses.Add(leagueResponse);
            }
            return responses;

        }

       
    }
}
