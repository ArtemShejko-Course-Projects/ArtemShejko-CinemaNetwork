﻿using System;
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
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<FilmSessions> FilmSessions { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<RoleTable> RoleTable { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<СinemaDetails> СinemaDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaStaff>()
                .Property(e => e.CinemaStaffSalary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<City>()
                .HasMany(e => e.СinemaDetails)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.СinemaDetailsCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Films>()
                .Property(e => e.FilmActors)
                .IsUnicode(false);

            modelBuilder.Entity<Films>()
                .Property(e => e.FilmImageUri)
                .IsUnicode(false);

            modelBuilder.Entity<Films>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Films)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FilmSessions>()
                .Property(e => e.SessionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FilmSessions>()
                .HasMany(e => e.Place)
                .WithRequired(e => e.FilmSessions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Films)
                .WithOptional(e => e.Genre)
                .HasForeignKey(e => e.FilmGenre);

            modelBuilder.Entity<Halls>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Halls)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Halls>()
                .HasMany(e => e.Place)
                .WithRequired(e => e.Halls)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.PlacePriceMultiplier)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleTable>()
                .HasMany(e => e.CinemaStaff)
                .WithRequired(e => e.RoleTable)
                .HasForeignKey(e => e.CinemaStaffRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleTable>()
                .HasMany(e => e.CinemaUser)
                .WithRequired(e => e.RoleTable)
                .HasForeignKey(e => e.CinemaUserRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<СinemaDetails>()
                .HasMany(e => e.Halls)
                .WithRequired(e => e.СinemaDetails)
                .WillCascadeOnDelete(false);
        }
    }
}
