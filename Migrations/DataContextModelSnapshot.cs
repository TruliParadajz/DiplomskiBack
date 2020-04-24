﻿// <auto-generated />
using System;
using BackendApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendApi.Entities.EventTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Draggable")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EndDt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Resizable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EventTasks");
                });

            modelBuilder.Entity("BackendApi.Entities.EventTaskNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppNotification")
                        .HasColumnType("int");

                    b.Property<int>("EmailNotification")
                        .HasColumnType("int");

                    b.Property<int>("EventTaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserNotificationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventTaskId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserNotificationId");

                    b.ToTable("EventTaskNotifications");
                });

            modelBuilder.Entity("BackendApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackendApi.Entities.UserNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AppNotification")
                        .HasColumnType("bit");

                    b.Property<bool>("EmailNotification")
                        .HasColumnType("bit");

                    b.Property<double>("Hours")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("BackendApi.Entities.EventTask", b =>
                {
                    b.HasOne("BackendApi.Entities.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendApi.Entities.EventTaskNotification", b =>
                {
                    b.HasOne("BackendApi.Entities.EventTask", "EventTask")
                        .WithMany()
                        .HasForeignKey("EventTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendApi.Entities.User", "User")
                        .WithOne("EventTaskNotification")
                        .HasForeignKey("BackendApi.Entities.EventTaskNotification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendApi.Entities.UserNotification", "UserNotification")
                        .WithMany()
                        .HasForeignKey("UserNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendApi.Entities.UserNotification", b =>
                {
                    b.HasOne("BackendApi.Entities.User", "User")
                        .WithOne("UserNotification")
                        .HasForeignKey("BackendApi.Entities.UserNotification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
