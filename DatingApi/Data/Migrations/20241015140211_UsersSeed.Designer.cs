﻿// <auto-generated />
using DatingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatingApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241015140211_UsersSeed")]
    partial class UsersSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("DatingApi.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Hany"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Basem"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mohamed"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
