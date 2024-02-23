using NetClubApi.Modules.MatchModule;
using NetClubApi.Model;

namespace NetClubApi.Modules.TeamModule
{
    public interface ITeamBusinessLogic
    {
        public Task<string> CreateTeam(TeamModel team);
        public Task<string> AddTeamMember(AddMember members);
    }

    public class TeamBusinessLogic:ITeamBusinessLogic
    {
        private readonly ITeamDataAccess _teamDataAccess;

        public TeamBusinessLogic(ITeamDataAccess teamDataAccess)
        {
            _teamDataAccess = teamDataAccess;
        }
        public async Task<string> CreateTeam(TeamModel team)
        {
            return await _teamDataAccess.CreateTeam(team);
        }

        public async Task<string> AddTeamMember(AddMember members)
        {
            return await _teamDataAccess.AddMember(members);
        }
    }
}
