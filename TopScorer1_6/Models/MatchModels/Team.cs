using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

[Table("Team")]
public partial class Team
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("AwayTeam")]
    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    [InverseProperty("HomeTeam")]
    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<TeamsLeague> TeamsLeagues { get; set; } = new List<TeamsLeague>();
}
