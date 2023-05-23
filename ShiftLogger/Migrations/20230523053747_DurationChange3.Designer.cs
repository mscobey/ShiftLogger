﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShiftLogger.Data;

#nullable disable

namespace ShiftLogger.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230523053747_DurationChange3")]
    partial class DurationChange3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShiftLogger.Shift", b =>
                {
                    b.Property<int>("shiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("shiftId"));

                    b.Property<TimeSpan>("Duration")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("time")
                        .HasComputedColumnSql("CONVERT(TIME,CONVERT(DATETIME, [End]) - CONVERT(DATETIME, [Start]))");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.HasKey("shiftId");

                    b.ToTable("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
