using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.LeagueModule
{


    public interface ILeagueDataAccess
    {
        public Task<string> CreateLeague(League league);
        public Task<List<League>> GetLeagues(int club_id);
    }
    public class LeagueDataAccess:ILeagueDataAccess
    {
        private readonly NetClubDbContext netClubDbContext;
        public LeagueDataAccess(NetClubDbContext netClubDbContext)
        {
            this.netClubDbContext = netClubDbContext;
        }

        public async Task<string> CreateLeague(League league)
        {
           await  netClubDbContext.AddAsync(league);
            netClubDbContext.SaveChanges();
            return "league created successfully";

        }

        public  async Task<List<League>> GetLeagues(int club_Id)
        {
            List<League> leagues =  await netClubDbContext.league.Where(league => league.ClubId == club_Id).ToListAsync();
            return leagues;
            
        }
    }
}
