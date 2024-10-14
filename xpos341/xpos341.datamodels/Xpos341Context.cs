using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

public partial class Xpos341Context : DbContext
{
    public Xpos341Context()
    {
    }

    public Xpos341Context(DbContextOptions<Xpos341Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblMenuAccess> TblMenuAccesses { get; set; }

    public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; }

    public virtual DbSet<TblOrderHeader> TblOrderHeaders { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblVariant> TblVariants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=XPOS_341;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblCateg__3214EC075E3A819D");
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblCusto__3214EC073DE4A0B7");
        });

        modelBuilder.Entity<TblMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblMenu__3214EC0715A3719C");
        });

        modelBuilder.Entity<TblMenuAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblMenuA__3214EC07ACDA12EF");
        });

        modelBuilder.Entity<TblOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblOrder__3214EC079E6CF721");
        });

        modelBuilder.Entity<TblOrderHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblOrder__3214EC07279E6305");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblProdu__3214EC07C833014C");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblRole__3214EC07C10BB237");
        });

        modelBuilder.Entity<TblVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblVaria__3214EC0787D5C4E8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
