using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace firstmvcproj.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<PragatiCustomer> PragatiCustomers { get; set; }

    public virtual DbSet<PragatiEmp> PragatiEmps { get; set; }

    public virtual DbSet<PragatiSbaccount> PragatiSbaccounts { get; set; }

    public virtual DbSet<PragatiSbtransaction> PragatiSbtransactions { get; set; }

    public virtual DbSet<PragatiUsertbl> PragatiUsertbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PragatiCustomer>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__pragatiC__C1FFD86112F2447B");

            entity.ToTable("pragatiCustomer");

            entity.Property(e => e.Cname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cname");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phno)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phno");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        modelBuilder.Entity<PragatiEmp>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__pragatiE__D9509F6D677ECB20");

            entity.ToTable("pragatiEmps");

            entity.Property(e => e.Eid)
                .ValueGeneratedNever()
                .HasColumnName("eid");
            entity.Property(e => e.Ename)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ename");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        modelBuilder.Entity<PragatiSbaccount>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__pragatiS__BE2ACD6E49144D7C");

            entity.ToTable("pragatiSBAccount");

            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PragatiSbtransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__pragatiS__55433A6B89FC3BA5");

            entity.ToTable("pragatiSBTransaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.PragatiSbtransactions)
                .HasForeignKey(d => d.AccountNumber)
                .HasConstraintName("FK__pragatiSB__Accou__5006DFF2");
        });

        modelBuilder.Entity<PragatiUsertbl>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__pragatiU__A9D105353367834A");

            entity.ToTable("pragatiUsertbl");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
