using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.LeagueModule
{
    public interface ILeagueBussinessLayer
    {
        public Task<List<LeagueResponse>> GetClubLeagues(int club_id);
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
            List<LeagueResponse> leagueResponses = ConvertToLeagueResponse(leagues);
            return leagueResponses;
        }

        private List<LeagueResponse> ConvertToLeagueResponse(List<League> leagues)
        {
            List<LeagueResponse> responses = new();
            foreach(League league in leagues)
            {
                LeagueResponse leagueResponse = new();
                leagueResponse.LeagueName = league.Name;
                leagueResponse.StartDate = league.StartDate;
                leagueResponse.EndDate = league.EndDate;
                leagueResponse.Teams = league.NumberOfTeams;
                leagueResponse.Matches = 0;
                responses.Add(leagueResponse);
            }
            return responses;

        }
    }
}
