using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

[Table("Match")]
public partial class Match
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("league_id")]
    public int? LeagueId { get; set; }

    [Column("match_date")]
    public DateOnly? MatchDate { get; set; }

    [Column("match_time")]
    [Precision(0)]
    public TimeOnly? MatchTime { get; set; }

    [Column("home_team_id")]
    public int HomeTeamId { get; set; }

    [Column("away_team_id")]
    public int AwayTeamId { get; set; }

    [Column("full_time_home_goal")]
    public int FullTimeHomeGoal { get; set; }

    [Column("full_time_away_goal")]
    public int FullTimeAwayGoal { get; set; }

    [Column("full_time_result_id")]
    public int? FullTimeResultId { get; set; }

    [Column("half_time_home_goal")]
    public int? HalfTimeHomeGoal { get; set; }

    [Column("half_time_away_goal")]
    public int? HalfTimeAwayGoal { get; set; }

    [Column("half_time_result_id")]
    public int? HalfTimeResultId { get; set; }

    [Column("referee_id")]
    public int? RefereeId { get; set; }

    [Column("home_shot")]
    public int? HomeShot { get; set; }

    [Column("away_shot")]
    public int? AwayShot { get; set; }

    [Column("home_shot_target")]
    public int? HomeShotTarget { get; set; }

    [Column("away_shot_target")]
    public int? AwayShotTarget { get; set; }

    [Column("home_foul")]
    public int? HomeFoul { get; set; }

    [Column("away_foul")]
    public int? AwayFoul { get; set; }

    [Column("home_corner")]
    public int? HomeCorner { get; set; }

    [Column("away_corner")]
    public int? AwayCorner { get; set; }

    [Column("home_yellow_card")]
    public int? HomeYellowCard { get; set; }

    [Column("away_yellow_card")]
    public int? AwayYellowCard { get; set; }

    [Column("home_red_card")]
    public int? HomeRedCard { get; set; }

    [Column("away_red_card")]
    public int? AwayRedCard { get; set; }

    [ForeignKey("AwayTeamId")]
    [InverseProperty("MatchAwayTeams")]
    public virtual Team AwayTeam { get; set; } = null!;

    [ForeignKey("FullTimeResultId")]
    [InverseProperty("MatchFullTimeResults")]
    public virtual Result? FullTimeResult { get; set; }


    [ForeignKey("HalfTimeResultId")]
    [InverseProperty("MatchHalfTimeResults")]
    public virtual Result? HalfTimeResult { get; set; }

    [ForeignKey("HomeTeamId")]
    [InverseProperty("MatchHomeTeams")]
    public virtual Team HomeTeam { get; set; } = null!;

    [ForeignKey("LeagueId")]
    [InverseProperty("Matches")]
    public virtual League League { get; set; }

    [ForeignKey("RefereeId")]
    [InverseProperty("Matches")]
    public virtual Referee? Referee { get; set; }
}
