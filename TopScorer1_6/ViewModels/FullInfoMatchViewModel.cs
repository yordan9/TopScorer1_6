namespace TopScorer1_6.ViewModels
{
    public class FullInfoMatchViewModel
    {
        public int Id { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateOnly? MatchDate { get; set; }
        public TimeOnly? MatchTime { get; set; }
        public int FTHG { get; set; }
        public int FTAG { get; set; }
        public string? FTLetter { get; set; }
        public int? HTHG { get; set; }
        public int? HTAG { get; set; }
        public string? HTLetter { get; set; }
        public string? Referee { get; set; }
        public int? HomeShot { get; set; }
        public int? AwayShot { get; set; }
        public int? HomeShotTarget { get; set; }
        public int? AwayShotTarget { get; set; }
        public int? HomeFoul { get; set; }
        public int? AwayFoul { get; set; }
        public int? HomeCorner { get; set; }
        public int? AwayCorner { get; set; }
        public int? HomeYellowCard { get; set; }
        public int? AwayYellowCard { get; set; }
        public int? HomeRedCard { get; set; }
        public int? AwayRedCard { get; set; }
    }
}
