﻿// <auto-generated />
using System;
using Geolocalizacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Geolocalizacao.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Clientes.ApiCliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataHoraInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("IdUsuarioAlteracao")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdUsuarioInclusao")
                        .HasColumnType("char(36)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(70)");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdUsuarioAlteracao");

                    b.HasIndex("IdUsuarioInclusao");

                    b.ToTable("ApiCliente");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Clientes.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("VARCHAR(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("VARCHAR(60)");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataHoraInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(254)");

                    b.Property<Guid?>("IdUsuarioAlteracao")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdUsuarioInclusao")
                        .HasColumnType("char(36)");

                    b.Property<string>("InscricaoMunicipal")
                        .IsRequired()
                        .HasColumnType("VARCHAR(18)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("VARCHAR(120)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Observacao")
                        .HasColumnType("VARCHAR(300)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("VARCHAR(300)");

                    b.Property<string>("Telefone1")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<string>("Telefone2")
                        .HasColumnType("VARCHAR(11)");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("CHAR(2)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
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

                    b.Property<string>("Erro")
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("IdUsuarioInclusao")
                        .HasColumnType("char(36)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NomeArquivo")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioInclusao");

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

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Clientes.ApiCliente", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Clientes.Cliente", "Cliente")
                        .WithMany("ApisCliente")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "Usuario_Alteracao")
                        .WithMany("ApiCliente_Alteracao")
                        .HasForeignKey("IdUsuarioAlteracao")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "Usuario_Inclusao")
                        .WithMany("ApiCliente_Inclusao")
                        .HasForeignKey("IdUsuarioInclusao")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Usuario_Alteracao");

                    b.Navigation("Usuario_Inclusao");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.SetoresCensitarios.ArquivoSetoresCensitarios", b =>
                {
                    b.HasOne("Geolocalizacao.Domain.Entities.Usuarios.Usuario", "UsuarioInclusao")
                        .WithMany("ArquivosSetoresCensitarios")
                        .HasForeignKey("IdUsuarioInclusao")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Clientes.Cliente", b =>
                {
                    b.Navigation("ApisCliente");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.PerfilUsuario", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Geolocalizacao.Domain.Entities.Usuarios.Usuario", b =>
                {
                    b.Navigation("ApiCliente_Alteracao");

                    b.Navigation("ApiCliente_Inclusao");

                    b.Navigation("ArquivosSetoresCensitarios");

                    b.Navigation("UsuarioVinculo");
                });
#pragma warning restore 612, 618
        }
    }
}
