using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CinemaDAL
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
            : base("name=CinemaContext")
        {
        }

        public virtual DbSet<CinemaStaff> CinemaStaff { get; set; }
        public virtual DbSet<CinemaUser> CinemaUser { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<FilmSessions> FilmSessions { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaStaff>()
                .Property(e => e.CinemaStaffSalary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Films>()
                .Property(e => e.FilmGenre)
                .IsUnicode(false);

            modelBuilder.Entity<Films>()
                .Property(e => e.FilmActors)
                .IsUnicode(false);

            modelBuilder.Entity<Films>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Films)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FilmSessions>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.FilmSessions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Halls>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Halls)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Halls>()
                .HasMany(e => e.Place)
                .WithRequired(e => e.Halls)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
