using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TopScorer1_6.Models.MatchModels;

//връзки и таблици от база FootballDB, добавена със Scaffolding
public partial class ApplicationDbContextMatches : DbContext
{
    public ApplicationDbContextMatches()
    {
    }

    public ApplicationDbContextMatches(DbContextOptions<ApplicationDbContextMatches> options)
        : base(options)
    {
    }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Referee> Referees { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamsLeague> TeamsLeagues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WINDOWS-D0AN80B\\SQLEXPRESS;Database=FootballDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__League__3213E83FED23CB8D");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Match__3213E83F53CE34EA");

            entity.Property(e => e.AwayRedCard).HasDefaultValue(0);
            entity.Property(e => e.AwayYellowCard).HasDefaultValue(0);
            entity.Property(e => e.HomeRedCard).HasDefaultValue(0);
            entity.Property(e => e.HomeYellowCard).HasDefaultValue(0);

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Match__away_team__6A30C649");

            entity.HasOne(d => d.FullTimeResult).WithMany(p => p.MatchFullTimeResults)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Match__full_time__6B24EA82");

            entity.HasOne(d => d.HalfTimeResult).WithMany(p => p.MatchHalfTimeResults)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Match__half_time__6C190EBB");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Match__home_team__693CA210");

            entity.HasOne(d => d.League).WithMany(p => p.Matches).HasConstraintName("FK__Match__league_id__68487DD7");

            entity.HasOne(d => d.Referee).WithMany(p => p.Matches).HasConstraintName("FK__Match__referee_i__6D0D32F4");
        });

        modelBuilder.Entity<Referee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Referee__3213E83F286AC6B2");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Result__3213E83F8575860B");

            entity.Property(e => e.Letter).IsFixedLength();
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3213E83F4AE8C60F");
        });

        modelBuilder.Entity<TeamsLeague>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams_Le__3213E83F7A1AE540");

            entity.HasOne(d => d.League)
                .WithMany(p => p.TeamsLeagues)
                .HasForeignKey(d => d.LeagueId)
                .HasConstraintName("FK_Teams_Leagues_Leagues");

            entity.HasOne(d => d.Team)
                .WithMany(p => p.TeamsLeagues)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Teams_Leagues_Teams");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
