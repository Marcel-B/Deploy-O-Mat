﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence.Migrations
{
    [DbContext(typeof(ExecutRContext))]
    [Migration("20200506225603_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models.CommandLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Caller")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<int>("ResultCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Success")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CommandLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
