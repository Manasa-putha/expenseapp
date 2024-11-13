﻿// <auto-generated />
using System;
using ExpenseApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241002061929_dataaa")]
    partial class dataaa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<int>("PaidById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PaidById");

                    b.ToTable("Expenses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100.00m,
                            Date = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2673),
                            Description = "Grocery shopping",
                            GroupId = 1,
                            IsSettled = false,
                            PaidById = 2
                        },
                        new
                        {
                            Id = 2,
                            Amount = 200.00m,
                            Date = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2674),
                            Description = "Dinner",
                            GroupId = 1,
                            IsSettled = false,
                            PaidById = 3
                        },
                        new
                        {
                            Id = 3,
                            Amount = 100.00m,
                            Date = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2675),
                            Description = "Lunch",
                            GroupId = 3,
                            IsSettled = false,
                            PaidById = 4
                        },
                        new
                        {
                            Id = 4,
                            Amount = 100.00m,
                            Date = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2677),
                            Description = "Tea",
                            GroupId = 3,
                            IsSettled = false,
                            PaidById = 2
                        });
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2636),
                            Description = "Expenses shared among roommates",
                            Email = "",
                            Name = "Roommates"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2637),
                            Description = "Office team lunch expenses",
                            Email = "",
                            Name = "Office Team"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2638),
                            Description = "Tour expenses",
                            Email = "",
                            Name = "Rishikesh Tour"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2639),
                            Description = "Kasi expenses",
                            Email = "",
                            Name = "Kasi Tour"
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2640),
                            Description = "Bangolore expenses",
                            Email = "",
                            Name = "Bangolre Daires"
                        });
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.GroupUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupUsers");

                    b.HasData(
                        new
                        {
                            UserId = 2,
                            GroupId = 1,
                            Email = "sai@gmail.com",
                            Id = 1
                        },
                        new
                        {
                            UserId = 3,
                            GroupId = 1,
                            Email = "manu@gmail.com",
                            Id = 2
                        },
                        new
                        {
                            UserId = 3,
                            GroupId = 2,
                            Email = "manu@gmail.com",
                            Id = 3
                        });
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2578),
                            Email = "admin@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Admin",
                            Password = "admin1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2597),
                            Email = "sai@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Sai",
                            Password = "sai1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2598),
                            Email = "manu@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Manu",
                            Password = "manu4321",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2599),
                            Email = "ramu@gmail.com",
                            MobileNumber = "123456789",
                            Name = "Ramu",
                            Password = "ramu4321",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2600),
                            Email = "raj@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Raj",
                            Password = "raj1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2601),
                            Email = "mice@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Mice",
                            Password = "sai1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 7,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2602),
                            Email = "kin@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Kin",
                            Password = "kin1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 8,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2603),
                            Email = "kapil@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Kapil",
                            Password = "kapil1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 9,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2604),
                            Email = "kesav@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Kesav",
                            Password = "kesav1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        },
                        new
                        {
                            Id = 10,
                            CreatedOn = new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2605),
                            Email = "Bob@gmail.com",
                            MobileNumber = "1234567890",
                            Name = "Bob",
                            Password = "bob1234",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        });
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.UserExpense", b =>
                {
                    b.Property<int>("UserExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserExpenseId"), 1L, 1);

                    b.Property<decimal>("AmountOwed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserExpenseId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExpenses");

                    b.HasData(
                        new
                        {
                            UserExpenseId = 1,
                            AmountOwed = 50.00m,
                            ExpenseId = 1,
                            IsSettled = false,
                            UserId = 2
                        },
                        new
                        {
                            UserExpenseId = 2,
                            AmountOwed = 50.00m,
                            ExpenseId = 1,
                            IsSettled = false,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.Expense", b =>
                {
                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.Group", "Group")
                        .WithMany("Expenses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.User", "PaidBy")
                        .WithMany()
                        .HasForeignKey("PaidById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("PaidBy");
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.GroupUser", b =>
                {
                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.UserExpense", b =>
                {
                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.Expense", "Expense")
                        .WithMany("SplitAmong")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Models.ExpenseApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.Expense", b =>
                {
                    b.Navigation("SplitAmong");
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.Group", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("GroupUsers");
                });

            modelBuilder.Entity("ExpenseApp.Models.ExpenseApp.Models.User", b =>
                {
                    b.Navigation("GroupUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
