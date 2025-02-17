﻿// <auto-generated />
using System;
using HomeVital.Repositories.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    [DbContext(typeof(HomeVitalDbContext))]
    [Migration("20250217223602_Measurements_2_Migration")]
    partial class Measurements_2_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeVital.Models.Entities.BloodPressure", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("BodyPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Diastolic")
                        .HasColumnType("integer");

                    b.Property<string>("MeasureHand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MeasurementID")
                        .HasColumnType("integer");

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.Property<int>("Pulse")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Systolic")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("MeasurementID");

                    b.ToTable("BloodPressures");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.Bloodsugar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<float>("BloodsugarLevel")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("MeasurementID")
                        .HasColumnType("integer");

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("MeasurementID");

                    b.ToTable("Bloodsugars");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.HealthcareWorker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("HealthcareWorkers");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.Measurement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.Patient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Kennitala")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.BloodPressure", b =>
                {
                    b.HasOne("HomeVital.Models.Entities.Measurement", null)
                        .WithMany("BloodPressure")
                        .HasForeignKey("MeasurementID");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.Bloodsugar", b =>
                {
                    b.HasOne("HomeVital.Models.Entities.Measurement", null)
                        .WithMany("BloodSugar")
                        .HasForeignKey("MeasurementID");
                });

            modelBuilder.Entity("HomeVital.Models.Entities.Measurement", b =>
                {
                    b.Navigation("BloodPressure");

                    b.Navigation("BloodSugar");
                });
#pragma warning restore 612, 618
        }
    }
}
