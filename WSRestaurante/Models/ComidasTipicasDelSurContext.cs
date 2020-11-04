using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WSRestaurante.Models
{
    public partial class ComidasTipicasDelSurContext : DbContext
    {
        public ComidasTipicasDelSurContext()
        {
        }

        public ComidasTipicasDelSurContext(DbContextOptions<ComidasTipicasDelSurContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Mesa> Mesa { get; set; }
        public virtual DbSet<Mesero> Mesero { get; set; }
        public virtual DbSet<Supervisor> Supervisor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=localhost;DataBase=ComidasTipicasDelSur;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("pk_cliente");

                entity.Property(e => e.Identificacion).ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.NroFactura)
                    .HasName("pk_detallefactura");

                entity.Property(e => e.NroFactura).ValueGeneratedNever();

                entity.Property(e => e.Plato)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("numeric(15, 2)");

                entity.HasOne(d => d.IdSupervisorNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdSupervisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_detallefactura_supervisor");

                entity.HasOne(d => d.NroFacturaNavigation)
                    .WithOne(p => p.DetalleFactura)
                    .HasForeignKey<DetalleFactura>(d => d.NroFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_detallefactura_factura");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.NroFactura)
                    .HasName("pk_factura");

                entity.Property(e => e.NroFactura).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factura_cliente");

                entity.HasOne(d => d.IdMeseroNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdMesero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factura_mesero");

                entity.HasOne(d => d.NroMesaNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.NroMesa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factura_mesa");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.HasKey(e => e.NroMesa)
                    .HasName("pk_mesa");

                entity.Property(e => e.NroMesa).ValueGeneratedNever();
            });

            modelBuilder.Entity<Mesero>(entity =>
            {
                entity.HasKey(e => e.IdMesero)
                    .HasName("pk_mesero");

                entity.Property(e => e.IdMesero).ValueGeneratedNever();

                entity.Property(e => e.Antiguedad).HasColumnType("datetime");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.HasKey(e => e.IdSupervisor)
                    .HasName("pk_supervisor");

                entity.Property(e => e.IdSupervisor).ValueGeneratedNever();

                entity.Property(e => e.Antiguedad).HasColumnType("datetime");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
