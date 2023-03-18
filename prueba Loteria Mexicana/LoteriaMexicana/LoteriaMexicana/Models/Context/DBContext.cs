using System;
using System.Collections.Generic;
using LoteriaMexicana.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoteriaMexicana.Models.Context;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cartas> Cartas { get; set; }

    public virtual DbSet<CartasPorTabla> CartasPorTabla { get; set; }

    public virtual DbSet<Tablas> Tablas { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=LoteriaMexicana;integrated security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cartas>(entity =>
        {
            entity.HasKey(e => e.IdCarta).HasName("PK__Cartas__D3C2E8F1F5838E39");

            entity.Property(e => e.IdCarta).HasColumnName("id_carta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CartasPorTabla>(entity =>
        {
            entity.HasKey(e => e.IdCartasPorTabla).HasName("PK__CartasPo__0DA8A098C4CB9617");

            entity.Property(e => e.IdCartasPorTabla).HasColumnName("id_CartasPorTabla");
            entity.Property(e => e.IdCarta).HasColumnName("id_carta");
            entity.Property(e => e.IdTabla).HasColumnName("id_tabla");
            entity.Property(e => e.PosicionC).HasColumnName("posicion_c");
            entity.Property(e => e.PosicionF).HasColumnName("posicion_f");

            entity.HasOne(d => d.IdCartaNavigation).WithMany(p => p.CartasPorTabla)
                .HasForeignKey(d => d.IdCarta)
                .HasConstraintName("FK__CartasPor__id_ca__54CB950F");

            entity.HasOne(d => d.IdTablaNavigation).WithMany(p => p.CartasPorTabla)
                .HasForeignKey(d => d.IdTabla)
                .HasConstraintName("FK__CartasPor__id_ta__55BFB948");
        });

        modelBuilder.Entity<Tablas>(entity =>
        {
            entity.HasKey(e => e.IdTabla).HasName("PK__Tablas__B8DC498267687062");

            entity.Property(e => e.IdTabla).HasColumnName("id_tabla");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
