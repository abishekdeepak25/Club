using NetClubApi.ClubModule;
using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;
using Org.BouncyCastle.Utilities;

namespace NetClubApi.MatchModule
{
    public interface IMatchBusinessLogic
    {
        public Task<string> CreateSchedule(MatchModel match);
    }
    public class MatchBusinessLogic : IMatchBusinessLogic
    {
        private readonly IMatchDataAccess _matchDataAccess;

        public MatchBusinessLogic(IMatchDataAccess matchDataAccess)
        {
            _matchDataAccess = matchDataAccess;
        }
        public async Task<string> CreateSchedule(MatchModel match)
        {
            return await _matchDataAccess.createMatch(match);

            //return await Integers(1);
        }
    }
}
