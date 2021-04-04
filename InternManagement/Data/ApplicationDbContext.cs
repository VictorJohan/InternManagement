using InternManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManagement.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Habilidade> Habilidades { get; set; }
        public virtual DbSet<Institucione> Instituciones { get; set; }
        public virtual DbSet<Nacionalidade> Nacionalidades { get; set; }
        public virtual DbSet<Pasante> Pasantes { get; set; }
        public virtual DbSet<PasanteHabilidade> PasanteHabilidades { get; set; }
        public virtual DbSet<AsignarTarea> AsignarTareas { get; set; }
        public virtual DbSet<RealizarTarea> RealizarTareas { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=proyectofinalap2.database.windows.net;Database=InternManagementApli2;User ID=InternManagement;Password=intern@Aplicada2;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Habilidade>(entity =>
            {
                entity.HasKey(e => e.HabilidadId)
                    .HasName("PK__Habilida__7341FE225BD54433");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            });

            modelBuilder.Entity<Institucione>(entity =>
            {
                entity.HasKey(e => e.InstitucionId)
                    .HasName("PK__Instituc__706D41C9FB0E0827");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nacionalidade>(entity =>
            {
                entity.HasKey(e => e.NacionalidadId)
                    .HasName("PK__Nacional__27A1D14C176866C9");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nacionalidad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pasante>(entity =>
            {
                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Institucion)
                    .WithMany(p => p.Pasantes)
                    .HasForeignKey(d => d.InstitucionId)
                    .HasConstraintName("FK__Pasantes__Instit__75A278F5");

                entity.HasOne(d => d.Nacionalidad)
                    .WithMany(p => p.Pasantes)
                    .HasForeignKey(d => d.NacionalidadId)
                    .HasConstraintName("FK__Pasantes__Nacion__76969D2E");
            });

            modelBuilder.Entity<PasanteHabilidade>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__PasanteH__3214EC077C62E519");
                
                entity.HasOne(d => d.Habilidad)
                    .WithMany()
                    .HasForeignKey(d => d.HabilidadId)
                    .HasConstraintName("FK__PasanteHa__Habil__7B5B524B");

                entity.HasOne(d => d.Pasante)
                    .WithMany()
                    .HasForeignKey(d => d.PasanteId)
                    .HasConstraintName("FK__PasanteHa__Pasan__7A672E12");
            });

            modelBuilder.Entity<RealizarTarea>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pasante)
                    .WithMany(p => p.RealizarTareas)
                    .HasForeignKey(d => d.PasanteId)
                    .HasConstraintName("FK__RealizarT__Pasan__04E4BC85");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.RealizarTareas)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("FK__RealizarT__Tarea__03F0984C");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Requerimiento)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoAproximado)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
