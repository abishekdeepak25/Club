using Microsoft.EntityFrameworkCore;
using NetClubApi.Model;
using NetClubApi.Model.ResponseModel;

namespace NetClubApi.Modules.LeagueModule
{


    public interface ILeagueDataAccess
    {
        public Task<string> CreateLeague(League league, int user_id);
        public Task<League> GetLeague(int league_id);
        public Task<List<League>> GetLeagues(int club_id);
        Task<int?> getLeagueTeams(int league_id);
        public Task<List<LeagueRegistration>> GetMyLeagues(int user_id);
        Task<int?> getNumberOfMatches(int league_id);
        public Task<bool> IsAdmin(int? club_id, int user_id);
        public Task<string> RegisterLeague(LeagueRegistration league);
        public Task<Club> GetClub(int club_id);
    }
    public class LeagueDataAccess : ILeagueDataAccess
    {
        private readonly NetClubDbContext netClubDbContext;
        public LeagueDataAccess(NetClubDbContext netClubDbContext)
        {
            this.netClubDbContext = netClubDbContext;
        }

        public async Task<string> CreateLeague(League league, int user_id)
        {
            try
            {
                if (await IsAdmin(league.club_id, user_id))
                {
                    await netClubDbContext.AddAsync(league);
                    netClubDbContext.SaveChanges();
                    return "league created successfully";
                }
                return "league not created";
            }
            catch (Exception)
            {
                throw;
            }



        }

        public async Task<bool> IsAdmin(int? club_id, int user_id)
        {
            var club = await netClubDbContext.club_registration.FirstOrDefaultAsync(club => club.club_id == club_id && club.user_id == user_id && club.isadmin);
            if (club == default)
                return false;
            return true;
        }

        public async Task<List<League>> GetLeagues(int club_Id)
        {
            List<League> leagues = await netClubDbContext.league.Where(league => league.club_id == club_Id).ToListAsync();
            return leagues;

        }

        public async Task<int?> getLeagueTeams(int league_id)
        {
            League league = await netClubDbContext.league.FirstOrDefaultAsync(league => league.Id == league_id);
            return league.number_of_teams;
        }

        public async Task<int?> getNumberOfMatches(int league_id)
        {
            return await netClubDbContext.league.CountAsync(league => league.Id == league_id);
        }

        public async Task<string> RegisterLeague(LeagueRegistration league)
        {
            try
            {
                await netClubDbContext.league_registration.AddAsync(league);
                await netClubDbContext.SaveChangesAsync();
                return "you register to the league";

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LeagueRegistration>> GetMyLeagues(int user_id)
        {
            return await netClubDbContext.league_registration.Where(league => league.user_id == user_id).ToListAsync();
        }

        public async Task<League> GetLeague(int league_id)
        {
            return await netClubDbContext.league.FirstOrDefaultAsync(league => league.Id == league_id);
        }

        public async Task<Club> GetClub(int club_id)
        {
            return await netClubDbContext.club.FirstAsync(club => club.Id == club_id);
        }
    }
}
