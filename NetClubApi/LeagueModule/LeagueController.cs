using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.LeagueModule
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly LeagueDataAccess _leagueDataAccess;
        private readonly LeagueBussinessLayer _leagueBussinessLayer;
        public LeagueController(LeagueDataAccess leagueDataAccess,LeagueBussinessLayer leagueBussinessLayer)
        {
            _leagueDataAccess = leagueDataAccess;
            _leagueBussinessLayer = leagueBussinessLayer;
        }
        [HttpPost]
        [Authorize]
        public async Task<string> CreateLeague(League league)
        {
            return await _leagueDataAccess.CreateLeague(league);
            
        }

        [HttpGet]
        [Authorize]
        public async Task<List<LeagueResponse>> GetClubLeagues(int club_Id)
        {
            return await _leagueBussinessLayer.GetClubLeagues(club_Id);
        }
        

    }
}
