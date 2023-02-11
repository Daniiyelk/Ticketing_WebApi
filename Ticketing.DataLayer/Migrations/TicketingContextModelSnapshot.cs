﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ticketing.DataLayer.Context;

#nullable disable

namespace Ticketing.DataLayer.Migrations
{
    [DbContext(typeof(TicketingContext))]
    partial class TicketingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ticketing.DataLayer.Entities.Admin.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Email = "Admin@gmail.com",
                            Password = "Admin"
                        });
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.Ticket.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Ticket");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTime = new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6240),
                            IsFinished = false,
                            Title = "رفع باگ",
                            userId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateTime = new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6246),
                            IsFinished = true,
                            Title = "مدیریت خطا ها",
                            userId = 1
                        });
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.TicketAnswer.TicketAnswer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("adminId")
                        .HasColumnType("int");

                    b.Property<int?>("questionId")
                        .HasColumnType("int");

                    b.Property<int>("ticketId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("adminId");

                    b.HasIndex("questionId");

                    b.HasIndex("ticketId");

                    b.ToTable("TicketAnswer");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Body = "در حال بررسی میباشد لطفا صبور باشید",
                            DateTime = new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6300),
                            adminId = 1,
                            questionId = 1,
                            ticketId = 1
                        });
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.TicketQuestion.TicketQuestion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsResponded")
                        .HasColumnType("bit");

                    b.Property<int>("ticketId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ticketId");

                    b.ToTable("TicketQuestion");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Body = "لطفا باگ صفحه ورود را برطرف کنید",
                            DateTime = new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6277),
                            IsResponded = true,
                            ticketId = 1
                        });
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "User1@gmail.com",
                            Name = "User1",
                            Password = "User1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "User2@gmail.com",
                            Name = "User2",
                            Password = "User2"
                        });
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.Ticket.Ticket", b =>
                {
                    b.HasOne("Ticketing.DataLayer.Entities.User.User", "User")
                        .WithMany("Ticket")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.TicketAnswer.TicketAnswer", b =>
                {
                    b.HasOne("Ticketing.DataLayer.Entities.Admin.Admin", "Admin")
                        .WithMany("TicketAnswer")
                        .HasForeignKey("adminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticketing.DataLayer.Entities.TicketQuestion.TicketQuestion", "TicketQuestion")
                        .WithMany()
                        .HasForeignKey("questionId");

                    b.HasOne("Ticketing.DataLayer.Entities.Ticket.Ticket", "Ticket")
                        .WithMany("TicketAnswers")
                        .HasForeignKey("ticketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Ticket");

                    b.Navigation("TicketQuestion");
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.TicketQuestion.TicketQuestion", b =>
                {
                    b.HasOne("Ticketing.DataLayer.Entities.Ticket.Ticket", "Ticket")
                        .WithMany("TicketQuestions")
                        .HasForeignKey("ticketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.Admin.Admin", b =>
                {
                    b.Navigation("TicketAnswer");
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.Ticket.Ticket", b =>
                {
                    b.Navigation("TicketAnswers");

                    b.Navigation("TicketQuestions");
                });

            modelBuilder.Entity("Ticketing.DataLayer.Entities.User.User", b =>
                {
                    b.Navigation("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}
