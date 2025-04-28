using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

[Table("League")]
public partial class League
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("League")]
    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    [InverseProperty("League")]
    public virtual ICollection<TeamsLeague> TeamsLeagues { get; set; } = new List<TeamsLeague>();
}
