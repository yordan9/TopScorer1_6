using Newtonsoft.Json;

namespace TopScorer1_6.Models
{
    //таблици за извличане на инфо от API-то
    public class FixturesResponse
    {
        [JsonProperty("matches")]
        public List<APIFixture> Matches { get; set; }
    }
    
    public class APIFixture
    {
        public int Id { get; set; }

        [JsonProperty("utcDate")]
        public string UtcDate { get; set; }
    
        [JsonProperty("competition")]
        public Competition Competition { get; set; }
    
        [JsonProperty("homeTeam")]
        public Team HomeTeam { get; set; }
    
        [JsonProperty("awayTeam")]
        public Team AwayTeam { get; set; }
    
        [JsonProperty("score")]
        public Score Score { get; set; }
    
        public DateTime LocalDateTime { get; set; } // изчисляваме го допълнително
    }
    
    public class Competition
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    
        [JsonProperty("emblem")]
        public string Emblem { get; set; }
    }
    
    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    
        [JsonProperty("shortName")]
        public string ShortName { get; set; }
    }
    
    public class Score
    {
        [JsonProperty("fullTime")]
        public Result FullTime { get; set; }
    }
    
    public class Result
    {
        [JsonProperty("home")]
        public int? Home { get; set; }
    
        [JsonProperty("away")]
        public int? Away { get; set; }
    }

    public class ChampionsLeagueResponse
    {
        public List<Match> Matches { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        public Competition Competition { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public DateTime UtcDate { get; set; }
    }

    public class FullTime
    {
        public int Home { get; set; }
        public int Away { get; set; }
    }
}
