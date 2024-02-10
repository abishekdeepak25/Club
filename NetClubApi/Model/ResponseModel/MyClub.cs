namespace NetClubApi.Model.ResponseModel
{
    public class MyClub:IClubResponse
    {


        public string Name { get; set; }
        public int? TotalLeagues { get; set; }
        public int Teams { get; set; }
        public int? ActiveLeagues { get; set; }
        
    }
}
