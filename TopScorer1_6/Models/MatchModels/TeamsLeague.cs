using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

[Table("Teams_Leagues")]
public partial class TeamsLeague
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("league_id")]
    public int? LeagueId { get; set; }

    [Column("team_id")]
    public int? TeamId { get; set; }

    [ForeignKey("LeagueId")]
    [InverseProperty("TeamsLeagues")]
    public virtual League? League { get; set; }

    [ForeignKey("TeamId")]
    [InverseProperty("TeamsLeagues")]
    public virtual Team? Team { get; set; }
}
