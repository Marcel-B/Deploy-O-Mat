﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Migrations
{
    [DbContext(typeof(InspectRContext))]
    partial class InspectRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Argument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("Arguments");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Port")
                        .HasColumnType("TEXT");

                    b.Property<int>("Replicas")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReplicasActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DockerServices");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DockerServiceDetails");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.EnvironmentVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("EnvironmentVariables");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Mount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("Target")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("Mounts");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Placement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("Placements");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Port", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Protocol")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublishMode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PublishedPort")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TargetPort")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.VirtualIp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DockerServiceDetailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NetworkId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DockerServiceDetailId");

                    b.ToTable("VirtualIps");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.VolumeOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MountId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MountId");

                    b.ToTable("VolumeOptions");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Argument", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("Arguments")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.EnvironmentVariable", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("EnvironmentVariables")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Label", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("Labels")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Mount", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", null)
                        .WithMany("Mounts")
                        .HasForeignKey("DockerServiceDetailId");
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Placement", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("Placements")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Port", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("Ports")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.VirtualIp", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.DockerServiceDetail", "DockerServiceDetail")
                        .WithMany("VirtualIps")
                        .HasForeignKey("DockerServiceDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.VolumeOption", b =>
                {
                    b.HasOne("com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models.Mount", "Mount")
                        .WithMany("VolumeOptions")
                        .HasForeignKey("MountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}