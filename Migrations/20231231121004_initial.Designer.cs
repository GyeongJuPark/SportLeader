﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportLeader.Infra.DB;

#nullable disable

namespace SportLeader.Migrations
{
    [DbContext(typeof(SpotrsLeaderDBContext))]
    [Migration("20231231121004_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SportLeader.Models.T_Certificate", b =>
                {
                    b.Property<string>("LeaderNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CertificateSequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateSequence"), 1L, 1);

                    b.Property<DateTime>("CertificateDT")
                        .HasColumnType("datetime2");

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertificateNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaderNo", "CertificateSequence");

                    b.ToTable("T_Certificate");
                });

            modelBuilder.Entity("SportLeader.Models.T_History", b =>
                {
                    b.Property<string>("LeaderNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("HistorySequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistorySequence"), 1L, 1);

                    b.Property<DateTime>("EndDT")
                        .HasColumnType("datetime2");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SportsNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDT")
                        .HasColumnType("datetime2");

                    b.HasKey("LeaderNo", "HistorySequence");

                    b.HasIndex("SportsNo");

                    b.ToTable("T_History");
                });

            modelBuilder.Entity("SportLeader.Models.T_Leader", b =>
                {
                    b.Property<string>("LeaderNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaderNo");

                    b.ToTable("T_Leader");
                });

            modelBuilder.Entity("SportLeader.Models.T_LeaderImage", b =>
                {
                    b.Property<string>("LeaderNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaderImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaderNo");

                    b.ToTable("T_LeaderImage");
                });

            modelBuilder.Entity("SportLeader.Models.T_LeaderWorkInfo", b =>
                {
                    b.Property<string>("LeaderNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EmpDT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeaderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SportsNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TelNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaderNo");

                    b.HasIndex("SchoolNo")
                        .IsUnique();

                    b.HasIndex("SportsNo")
                        .IsUnique();

                    b.ToTable("T_LeaderWorkInfo");
                });

            modelBuilder.Entity("SportLeader.Models.T_School", b =>
                {
                    b.Property<string>("SchoolNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolNo");

                    b.ToTable("T_School");
                });

            modelBuilder.Entity("SportLeader.Models.T_Sport", b =>
                {
                    b.Property<string>("SportsNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SportsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SportsNo");

                    b.ToTable("T_Sport");
                });

            modelBuilder.Entity("SportLeader.Models.T_Certificate", b =>
                {
                    b.HasOne("SportLeader.Models.T_LeaderWorkInfo", "T_LeaderWorkInfo")
                        .WithMany("T_Certificate")
                        .HasForeignKey("LeaderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("T_LeaderWorkInfo");
                });

            modelBuilder.Entity("SportLeader.Models.T_History", b =>
                {
                    b.HasOne("SportLeader.Models.T_LeaderWorkInfo", "T_LeaderWorkInfo")
                        .WithMany("T_History")
                        .HasForeignKey("LeaderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportLeader.Models.T_Sport", "T_Sport")
                        .WithMany()
                        .HasForeignKey("SportsNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("T_LeaderWorkInfo");

                    b.Navigation("T_Sport");
                });

            modelBuilder.Entity("SportLeader.Models.T_LeaderImage", b =>
                {
                    b.HasOne("SportLeader.Models.T_LeaderWorkInfo", null)
                        .WithOne("T_LeaderImage")
                        .HasForeignKey("SportLeader.Models.T_LeaderImage", "LeaderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportLeader.Models.T_LeaderWorkInfo", b =>
                {
                    b.HasOne("SportLeader.Models.T_Leader", "T_Leader")
                        .WithOne()
                        .HasForeignKey("SportLeader.Models.T_LeaderWorkInfo", "LeaderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportLeader.Models.T_School", "T_School")
                        .WithOne()
                        .HasForeignKey("SportLeader.Models.T_LeaderWorkInfo", "SchoolNo")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SportLeader.Models.T_Sport", "T_Sport")
                        .WithOne()
                        .HasForeignKey("SportLeader.Models.T_LeaderWorkInfo", "SportsNo")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("T_Leader");

                    b.Navigation("T_School");

                    b.Navigation("T_Sport");
                });

            modelBuilder.Entity("SportLeader.Models.T_LeaderWorkInfo", b =>
                {
                    b.Navigation("T_Certificate");

                    b.Navigation("T_History");

                    b.Navigation("T_LeaderImage")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
