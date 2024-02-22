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
        private readonly ILeagueDataAccess _leagueDataAccess;
        private readonly ILeagueBussinessLayer _leagueBussinessLayer;
        public LeagueController(ILeagueDataAccess leagueDataAccess,ILeagueBussinessLayer leagueBussinessLayer)
        {
            _leagueDataAccess = leagueDataAccess;
            _leagueBussinessLayer = leagueBussinessLayer;
        }
        [HttpPost]
        [Authorize]

       //only admin can create the createLeague
        public async Task<string> CreateLeague(League league)

        {
            int user_id = int.Parse(User.FindFirst("id").Value);
            var userClaims = User.FindFirst("id");
            var id = userClaims.Value;
            Console.WriteLine(int.Parse(id));
            //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFiaXNoZWs1QGdtYWlsLmNvbSIsImlkIjoiMjUiLCJuYmYiOjE3MDg2MTAyOTEsImV4cCI6MTcwOTIxNTA5MSwiaWF0IjoxNzA4NjEwMjkxfQ.903sKFWgnHIMR5InrYQv - lCEUtoN15lBqV4Eq1KoihE
            /* {
                 "id": 0,
   "name": "Abishek",
   "club_id": 38,
   "start_date": "2024-02-22T14:21:47.108Z",
   "end_date": "2024-02-28T14:21:47.108Z",
   "league_type_id": 1,
   "schedule_type_id": 1,
   "number_of_teams": 4,
   "number_of_teams_playoffs": 2,
   "playoff_start_date": "2024-02-25T14:21:47.108Z",
   "playoff_end_date": "2024-02-27T14:21:47.108Z",
   "playoff_type_id": 1,
   "registration_start_date": "2024-02-22T14:21:47.108Z",
   "registration_end_date": "2024-02-25T14:21:47.108Z"
 }*/
            //return "Abishek";
            return await _leagueDataAccess.CreateLeague(league, int.Parse(id));
            
        }

        [HttpGet]
        [Authorize]
        public async Task<List<LeagueResponse>> GetClubLeagues(int club_Id)
        {
            return await _leagueBussinessLayer.GetClubLeagues(club_Id);
        }

        [HttpGet]
        [Authorize]
        public async Task<int?> GetLeagueTeams(int league_id)
        {
            return await _leagueBussinessLayer.GetLeagueTeams(league_id);
        }

        [HttpPost]
        [Authorize]
        public async Task<string> RegisterLeague(LeagueRegistration league)
        {
            league.user_id = int.Parse(User.FindFirst("id").Value);

            return await _leagueBussinessLayer.RegisterLeague(league);
        }
        [HttpGet]
        [Authorize]
        public async Task<List<MyLeagues>> MyLeagues()
        {
            int user_id = int.Parse(User.FindFirst("id").Value);
            return await _leagueBussinessLayer.GetMyLeagues(user_id);

        }

    }
}
