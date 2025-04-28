namespace TopScorer1_6.ViewModels
{
    public class MatchGridViewModel
    {
        public List<string> Teams { get; set; }
        public Dictionary<(string Home, string Away), string> Results { get; set; }
        public Dictionary<string, string> TeamAbbreviations { get; set; }
    }
}
