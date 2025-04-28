using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

[Table("Result")]
public partial class Result
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("letter")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Letter { get; set; }

    [InverseProperty("FullTimeResult")]
    public virtual ICollection<Match> MatchFullTimeResults { get; set; } = new List<Match>();

    [InverseProperty("HalfTimeResult")]
    public virtual ICollection<Match> MatchHalfTimeResults { get; set; } = new List<Match>();
}
