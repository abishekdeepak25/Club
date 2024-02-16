using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NetClubApi.Comman;
using NetClubApi.Helper;
using NetClubApi.Model;

namespace NetClubApi.MatchModel
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        [HttpPost]
        //[Authorize]
        public async Task<string> CreateSchedule(int league_id)
        {
            return "";
        }

        [HttpGet]
        //[Authorize]
        public async Task<string> GetSchedule(int league_id)
        {
            return "";
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetStandings(int league_id)
        {
            return "";
        }

        [HttpPut]
        [Authorize]
        public async Task<string> SaveScore(int score)
        {
            return "";
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetScore(int match_id)
        {
            return "";
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetMyMatches()
        {
            return "";
        }
    }
}
