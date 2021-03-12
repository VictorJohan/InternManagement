using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InternManagement.Models
{
    public partial class Contexto : IdentityDbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<AsignarTarea> AsignarTareas { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Habilidade> Habilidades { get; set; }
        public virtual DbSet<Institucione> Instituciones { get; set; }
        public virtual DbSet<Nacionalidade> Nacionalidades { get; set; }
        public virtual DbSet<Pasante> Pasantes { get; set; }
        public virtual DbSet<PasanteHabilidade> PasanteHabilidades { get; set; }
        public virtual DbSet<RealizarTarea> RealizarTareas { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=proyectofinalap2.database.windows.net;Database=InternManagementApli2;User ID=InternManagement;Password=intern@Aplicada2;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AsignarTarea>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Pasante)
                    .WithMany(p => p.AsignarTareas)
                    .HasForeignKey(d => d.PasanteId)
                    .HasConstraintName("FK__AsignarTa__Pasan__01142BA1");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.AsignarTareas)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("FK__AsignarTa__Tarea__00200768");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

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

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
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
                entity.HasNoKey();

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
