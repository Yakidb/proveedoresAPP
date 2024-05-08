using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using proveedoresAPP.Models;


//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 7, 2023.

namespace proveedoresAPP.Data;

public partial class ProveedoresDbContext : DbContext
{
    public ProveedoresDbContext()
    {
    }

    public ProveedoresDbContext(DbContextOptions<ProveedoresDbContext> options)
        : base(options)
    {
    }

    public DbSet<prodentityProductoEntityDB> Productos { get; set; }

    public DbSet<proventityProveedorEntityDB> Proveedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<proventityProveedorEntityDB>()
            .HasMany(p => p.Productos)
            .WithOne(p => p.ProventityProveedorEntity)
            .HasForeignKey(p => p.IdProveedor)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<prodentityProductoEntityDB>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A13221BA1A83");

            entity.HasIndex(e => e.IdProveedor, "IX_Proveedor_Producto");

            entity.HasIndex(e => e.Codigo, "UQ__Producto__06370DACC9CF2A81").IsUnique();

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Unidad).HasMaxLength(3);

            entity.HasOne(d => d.ProventityProveedorEntity).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Proveedor_Producto");
        });


        modelBuilder.Entity<proventityProveedorEntityDB>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B1F1A4BFB");

            entity.HasIndex(e => e.Codigo, "UQ__Proveedo__06370DACA1128A2C").IsUnique();

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.RazonSocial).HasMaxLength(150);
            entity.Property(e => e.RFC)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RFC");
        });

        OnModelCreatingPartial(modelBuilder);
        
    }

    //--------------------------------------------------------------------------------------------------------------
    public IDbContextTransaction dbtranGet(
        )
    {
        return this.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
