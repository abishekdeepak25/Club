using System.ComponentModel.DataAnnotations;

namespace NetClubApi.Model
{
    public class MatchModel
    {

        [Key]
        public int match_id { get; set; }
        public int club_id { get; set; }
        public int league_id { get; set; }

        public int team1_id{ get; set; }
        public int  team2_id{ get; set; }
        public int player1_id { get; set; }
        public int player2_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int court_id { get; set; }

        public int point { get; set; }
        public int rating { get; set; }
    }
}
