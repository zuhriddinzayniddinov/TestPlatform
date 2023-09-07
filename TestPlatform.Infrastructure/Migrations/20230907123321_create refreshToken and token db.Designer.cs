﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestPlatform.Infrastructure.Contexts;

#nullable disable

namespace TestPlatform.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230907123321_create refreshToken and token db")]
    partial class createrefreshTokenandtokendb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TestPlatform.Domain.Entities.Authentication.RefreshToken", b =>
                {
                    b.Property<string>("DeviceRefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceRefreshToken");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("TestPlatform.Domain.Entities.Authentication.Token", b =>
                {
                    b.Property<string>("DeviceToken")
                        .HasColumnType("text");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceToken");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("TestPlatform.Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
