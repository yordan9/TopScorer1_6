namespace TopScorer1_6.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public DateOnly? MatchDate { get; set; }
        public TimeOnly? MatchTime { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public int? FullTimeHomeGoal { get; set; }
        public int? FullTimeAwayGoal { get; set; }
    }
}
