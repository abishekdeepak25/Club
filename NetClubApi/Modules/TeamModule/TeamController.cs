using Microsoft.AspNetCore.Mvc;
using NetClubApi.MatchModule;
using NetClubApi.Model;

namespace NetClubApi.Modules.TeamModule
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamBusinessLogic _teamBussinessLogics;

        public TeamController(ITeamBusinessLogic teamBussinessLogic)
        {
            _teamBussinessLogics = teamBussinessLogic;
        }
        [HttpPost]
        public async Task<string> CreateTeam(TeamModel team)
        {
            return await _teamBussinessLogics.CreateTeam(team);
;       }

        [HttpPost]
        public async Task<string> AddTeamMembers(AddMember members)
        {
            return await _teamBussinessLogics.AddTeamMember(members);
        }
    }
}
