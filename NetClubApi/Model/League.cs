using System.ComponentModel.DataAnnotations;

namespace NetClubApi.Model
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClubId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LeagueTypeId { get; set; }
        public int? ScheduleTypeId { get; set; }
        public int? NumberOfTeams { get; set; }
        public int? NumberOfTeamsPlayoffs { get; set; }
        public DateTime? PlayoffStartDate { get; set; }
        public DateTime? PlayoffEndDate { get; set; }
        public int? PlayoffTypeId { get; set; }
        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
    }
}
