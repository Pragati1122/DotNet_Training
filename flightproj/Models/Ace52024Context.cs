using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace flightproj.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<PragatiBooking> PragatiBookings { get; set; }

    public virtual DbSet<PragatiFlight> PragatiFlights { get; set; }

    public virtual DbSet<PragatiFlightUser> PragatiFlightUsers { get; set; }

    public virtual DbSet<PragatiPassenger> PragatiPassengers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PragatiBooking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("PK__pragatiB__73961EC5CC2F52EC");

            entity.ToTable("pragatiBooking");

            entity.Property(e => e.Bookingdate).HasColumnType("datetime");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Flight).WithMany(p => p.PragatiBookings)
                .HasForeignKey(d => d.Flightid)
                .HasConstraintName("FK__pragatiBo__Total__76818E95");

            entity.HasOne(d => d.Passenger).WithMany(p => p.PragatiBookings)
                .HasForeignKey(d => d.Passengerid)
                .HasConstraintName("FK__pragatiBo__Passe__7775B2CE");
        });

        modelBuilder.Entity<PragatiFlight>(entity =>
        {
            entity.HasKey(e => e.Flightid).HasName("PK__pragatiF__8A990066EB6CBE6C");

            entity.ToTable("pragatiFlight");

            entity.Property(e => e.Destination)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Flightname)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Layover)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PragatiFlightUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__pragatiF__536C85E5A770B79E");

            entity.ToTable("pragatiFlightUser");

            entity.HasIndex(e => e.Email, "UQ__pragatiF__A9D10534F9A6791D").IsUnique();

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PragatiPassenger>(entity =>
        {
            entity.HasKey(e => e.Passengerid).HasName("PK__pragatiP__88905388A7D06D4B");

            entity.ToTable("pragatiPassenger");

            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.PragatiPassengers)
                .HasForeignKey(d => d.Flightid)
                .HasConstraintName("FK__pragatiPa__Fligh__31A25463");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.PragatiPassengers)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__pragatiPa__Usern__73A521EA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
