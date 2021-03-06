// <auto-generated />
using System;
using Logs.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logs.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220501091247_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.24");

            modelBuilder.Entity("Logs.BLL.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("LogTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LogTypeId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Logs.BLL.Entities.LogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Information"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Warning"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Error"
                        });
                });

            modelBuilder.Entity("Logs.BLL.Entities.Log", b =>
                {
                    b.HasOne("Logs.BLL.Entities.LogType", "LogType")
                        .WithMany()
                        .HasForeignKey("LogTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
