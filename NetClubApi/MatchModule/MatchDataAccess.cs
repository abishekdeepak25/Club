using Microsoft.Data.SqlClient;
using NetClubApi.Helper;
using NetClubApi.Model;

namespace NetClubApi.MatchModule
{
    public interface IMatchDataAccess
    {
        public Task<string> createMatch(MatchModel match);
        
    }
    public class MatchDataAccess : IMatchDataAccess
    {
        public Task<string> createMatch(MatchModel match)
        {
            try
            {
                using (SqlConnection myCon = sqlHelper.GetConnection())
                {
                    myCon.Open();
                    string sql1 = $@"INSERT INTO [dbo].[match] (club_id, league_id, team1_id, team2_id,player1_id,player2_id,start_date,end_date,court_id)
                                   VALUES ('{match.club_id}','{match.league_id}','{match.team1_id}',    '{match.team2_id}','{match.player1_id}','{match.player2_id}','{match.start_date}','{match.end_date}','{match.court_id}')";
                     using (SqlCommand myCommand1 = new SqlCommand(sql1, myCon))
                     {
                         myCommand1.ExecuteNonQuery();
                     }
                        myCon.Close();
                }
                return Task.FromResult("Success");
            }
            catch (Exception ex)
            {
               return Task.FromResult("Failed"+ex.Message);
            }
        }
    }
}
