﻿// <auto-generated />
using System;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFramework.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20210331181150_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("types.Arm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Length")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Arms");
                });

            modelBuilder.Entity("types.Finger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DigitName")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("HandId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HandId");

                    b.ToTable("Fingers");
                });

            modelBuilder.Entity("types.Hand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ArmId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Chirality")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArmId");

                    b.ToTable("Hands");
                });

            modelBuilder.Entity("types.Finger", b =>
                {
                    b.HasOne("types.Hand", "Hand")
                        .WithMany("Fingers")
                        .HasForeignKey("HandId");

                    b.Navigation("Hand");
                });

            modelBuilder.Entity("types.Hand", b =>
                {
                    b.HasOne("types.Arm", "Arm")
                        .WithMany("Hands")
                        .HasForeignKey("ArmId");

                    b.Navigation("Arm");
                });

            modelBuilder.Entity("types.Arm", b =>
                {
                    b.Navigation("Hands");
                });

            modelBuilder.Entity("types.Hand", b =>
                {
                    b.Navigation("Fingers");
                });
#pragma warning restore 612, 618
        }
    }
}
