using System.ComponentModel.DataAnnotations;

namespace TopScorer1_6.ViewModels
{
    public class AdminPanelViewModel
    {
        [Required]
        public int LeagueId { get; set; }

        [Required]
        public string MatchDate { get; set; } // За DateOnly.Parse()

        [Required]
        public string MatchTime { get; set; } // За TimeOnly.Parse()

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int HalfTimeHomeGoal { get; set; }

        [Required]
        public int HalfTimeAwayGoal { get; set; }

        [Required]
        public int HalfTimeResultId { get; set; }

        [Required]
        public int FullTimeHomeGoal { get; set; }

        [Required]
        public int FullTimeAwayGoal { get; set; }

        [Required]
        public int FullTimeResultId { get; set; }

        [Required]
        public int RefereeId { get; set; }

        [Required]
        public int HomeShot { get; set; }

        [Required]
        public int AwayShot { get; set; }

        [Required]
        public int HomeShotTarget { get; set; }

        [Required]
        public int AwayShotTarget { get; set; }

        [Required]
        public int HomeFoul { get; set; }

        [Required]
        public int AwayFoul { get; set; }

        [Required]
        public int HomeCorner { get; set; }

        [Required]
        public int AwayCorner { get; set; }

        [Required]
        public int HomeYellowCard { get; set; }

        [Required]
        public int AwayYellowCard { get; set; }

        [Required]
        public int HomeRedCard { get; set; }

        [Required]
        public int AwayRedCard { get; set; }
    }
}
