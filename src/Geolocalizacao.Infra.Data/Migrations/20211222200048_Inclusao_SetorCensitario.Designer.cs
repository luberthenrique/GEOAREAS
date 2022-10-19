﻿// <auto-generated />
using System;
using Geolocalizacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace Geolocalizacao.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211222200048_Inclusao_SetorCensitario")]
    partial class Inclusao_SetorCensitario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.AreasAbrangencia.AreaAbrangencia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("IdMunicipio")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("IdUsuarioAlteracao")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdUsuarioInclusao")
                        .HasColumnType("char(36)");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<string>("Localidade")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Uf")
                        .HasColumnType("CHAR(2)");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioAlteracao");

                    b.HasIndex("IdUsuarioInclusao");

                    b.ToTable("AreaAbrangencia");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.AreasAbrangencia.Setor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("OGR_FID");

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cd_setor");

                    b.Property<string>("CodigoDistrito")
                        .HasColumnType("varchar(9)")
                        .HasColumnName("cd_dist");

                    b.Property<string>("CodigoMunicipio")
                        .HasColumnType("varchar(7)")
                        .HasColumnName("cd_mun");

                    b.Property<string>("CodigoSituacao")
                        .HasColumnType("varchar(1)")
                        .HasColumnName("cd_sit");

                    b.Property<string>("CodigoSubDistrito")
                        .HasColumnType("varchar(11)")
                        .HasColumnName("cd_subdist");

                    b.Property<string>("CodigoUf")
                        .HasColumnType("varchar(2)")
                        .HasColumnName("cd_uf");

                    b.Property<Geometry>("Geometry")
                        .IsRequired()
                        .HasColumnType("geometry")
                        .HasColumnName("SHAPE");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(60)")
                        .HasColumnName("nm_mun");

                    b.Property<string>("NomeDistrito")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nm_dist");

                    b.Property<string>("NomeSubDistrito")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nm_subdist");

                    b.Property<string>("NomeUf")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nm_uf");

                    b.Property<string>("Situacao")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nm_sit");

                    b.Property<string>("Uf")
                        .HasColumnType("varchar(2)")
                        .HasColumnName("sigla_uf");

                    b.HasKey("Id");

                    b.HasIndex("Geometry")
                        .HasAnnotation("MySql:SpatialIndex", true);

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("bairros");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.SetoresCensitarios.ArquivoSetoresCensitarios", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("IdUsuarioInclusao")
                        .HasColumnType("char(36)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeArquivo")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UsuarioInclusaoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioInclusaoId");

                    b.ToTable("ArquivoSetoresCensitarios");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("IdImagem")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdPerfil")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("IdUsuario")
                        .HasColumnType("char(36)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("DateTime");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IdPerfil");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.AreasAbrangencia.AreaAbrangencia", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "Usuario_Alteracao")
                        .WithMany("AreaAbrangencia_Alteracao")
                        .HasForeignKey("IdUsuarioAlteracao")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "Usuario_Inclusao")
                        .WithMany("AreaAbrangencia_Inclusao")
                        .HasForeignKey("IdUsuarioInclusao")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario_Alteracao");

                    b.Navigation("Usuario_Inclusao");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.SetoresCensitarios.ArquivoSetoresCensitarios", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "UsuarioInclusao")
                        .WithMany("ArquivosSetoresCensitarios")
                        .HasForeignKey("UsuarioInclusaoId");

                    b.Navigation("UsuarioInclusao");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", "PerfilUsuario")
                        .WithMany("Usuario")
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "UsuarioGestor")
                        .WithMany("UsuarioVinculo")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PerfilUsuario");

                    b.Navigation("UsuarioGestor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.Navigation("AreaAbrangencia_Alteracao");

                    b.Navigation("AreaAbrangencia_Inclusao");

                    b.Navigation("ArquivosSetoresCensitarios");

                    b.Navigation("UsuarioVinculo");
                });
#pragma warning restore 612, 618
        }
    }
}
