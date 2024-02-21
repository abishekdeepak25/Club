using Microsoft.Data.SqlClient;
using NetClubApi.Helper;
using NetClubApi.Model;
using System.Text.RegularExpressions;

namespace NetClubApi.Modules.TeamModule
{
    public interface ITeamDataAccess
    {
        public Task<string> CreateTeam(TeamModel team);

        public Task<string> AddMember(AddMember members);
    }
    public class TeamDataAccess:ITeamDataAccess
    {
        public Task<string> CreateTeam(TeamModel team)
        {
            try
            {
                using (SqlConnection myCon = sqlHelper.GetConnection())
                {
                    myCon.Open();
                    string sql1 = $@"INSERT INTO [dbo].[team] (club_id, league_id, team_name, court_id,points,rating)
                                   VALUES ('{team.club_id}','{team.league_id}','{team.team_name}',    '{team.court_id}','{team.points}','{team.rating}')";
                    using (SqlCommand myCommand1 = new SqlCommand(sql1, myCon))
                    {
                        myCommand1.ExecuteNonQuery();
                    }
                    myCon.Close();
                }
                return Task.FromResult("Team created");
            }catch(Exception ex)
            {
                return Task.FromResult("Team creation failed");
            }
        }

        public Task<string> AddMember(AddMember team)
        {

            try
            {
                List<int>members= team.team_member_user_id.ToList();
                int member_count = members.Count;
                if (member_count > 0)
                {
                    using (SqlConnection myCon = sqlHelper.GetConnection())
                    {
                        myCon.Open();
                        for (int i = 0; i < member_count; i++)
                        {
                            string sql1 = $@"INSERT INTO [dbo].[team_member] (team_member_user_id, team_id)
                                   VALUES ('{members[i]}','{team.team_id}')";
                            using (SqlCommand myCommand1 = new SqlCommand(sql1, myCon))
                            {
                                myCommand1.ExecuteNonQuery();
                            }
                        }
                        myCon.Close();
                    }
                }
                return Task.FromResult("Team member added");
            }
            catch (Exception ex)
            {
                return Task.FromResult($"Team creation not added{ex.Message}");
            }
        }
    }
}


/* using (SqlConnection myCon = sqlHelper.GetConnection())
 {
     myCon.Open();
     string sql1 = $@"INSERT INTO [dbo].[team_member] (team_member_user_id, team_id)
                    VALUES ('{team.team_member_user_id[0]}','{team.team_id}')";
     using (SqlCommand myCommand1 = new SqlCommand(sql1, myCon))
     {
         myCommand1.ExecuteNonQuery();
     }
     myCon.Close();
 }*/
