﻿// <auto-generated />
using System;
using GzReservation.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GzReservation.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240129200222_gzreservation1")]
    partial class gzreservation1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GzReservation.Shared.Entity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("entity_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("max_day")
                        .HasColumnType("integer");

                    b.Property<int>("max_week")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("entities");
                });

            modelBuilder.Entity("GzReservation.Shared.Form", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("a1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a10")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a11")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a12")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a13")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a14")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a15")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a16")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a17")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a4")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a5")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a6")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a7")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a8")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("a9")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("actiondate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("bagde_color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("birthdate")
                        .HasColumnType("date");

                    b.Property<bool>("deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("did")
                        .HasColumnType("integer");

                    b.Property<int?>("doc_no")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("entity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("father_work")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("grad_year")
                        .HasColumnType("date");

                    b.Property<long?>("id_number")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<int?>("info_book")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("mother_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mother_work")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nationalism")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("old_place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("passport_no")
                        .HasColumnType("integer");

                    b.Property<long?>("phone1")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("phone2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_city")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_dar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_govern")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_mhala")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_point")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("place_zuqaq")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("religion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("review_date")
                        .HasColumnType("date");

                    b.Property<int?>("seq")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("study")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("wife_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("wife_work")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("work_place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("work_place2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("forms");
                });

            modelBuilder.Entity("GzReservation.Shared.Reservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("EntityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("action_date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("doc_no")
                        .HasColumnType("integer");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mother_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("reservation_date")
                        .HasColumnType("date");

                    b.HasKey("id");

                    b.HasIndex("EntityId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("GzReservation.Shared.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("usersentity");
                });

            modelBuilder.Entity("GzReservation.Shared.Reservation", b =>
                {
                    b.HasOne("GzReservation.Shared.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });
#pragma warning restore 612, 618
        }
    }
}
