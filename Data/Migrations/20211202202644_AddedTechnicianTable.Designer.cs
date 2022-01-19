﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NestLinkV2.Data;

namespace NestLinkV2.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211202202644_AddedTechnicianTable")]
    partial class AddedTechnicianTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NestLinkV2.Models.Assignment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SiteId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SiteId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("NestLinkV2.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NestLinkV2.Models.Complaint", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("NestLinkV2.Models.ComplaintEventHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("ComplaintId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("ComplaintEventHistories");
                });

            modelBuilder.Entity("NestLinkV2.Models.EventType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("EventTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Job"
                        });
                });

            modelBuilder.Entity("NestLinkV2.Models.Job", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueWhen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ETA")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JobStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("Decimal(18, 2)");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("JobStatusId");

                    b.HasIndex("ProductId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("NestLinkV2.Models.JobEventHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("JobId");

                    b.ToTable("JobEventHistories");
                });

            modelBuilder.Entity("NestLinkV2.Models.JobStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("JobStatuses");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Job Created"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Picked Up"
                        },
                        new
                        {
                            ID = 3,
                            Name = "On Site"
                        });
                });

            modelBuilder.Entity("NestLinkV2.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("Decimal(18, 2)");

                    b.HasKey("ID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "This is a test product",
                            Name = "Test Product",
                            Price = 2.15m
                        });
                });

            modelBuilder.Entity("NestLinkV2.Models.Quote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("NestLinkV2.Models.QuoteEventHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("QuoteId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuoteEventHistories");
                });

            modelBuilder.Entity("NestLinkV2.Models.Site", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ClientId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("NestLinkV2.Models.Technician", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CurrentJobId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("CurrentJobId");

                    b.ToTable("Technicians");
                });

            modelBuilder.Entity("NestLinkV2.Models.WorkItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("NestLinkV2.Models.WorkItemEventHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkItemId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("WorkItemEventHistories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NestLinkV2.Models.Assignment", b =>
                {
                    b.HasOne("NestLinkV2.Models.Site", "Site")
                        .WithMany("Assignments")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("NestLinkV2.Models.Complaint", b =>
                {
                    b.HasOne("NestLinkV2.Models.Assignment", "Assignment")
                        .WithMany("Complaints")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("NestLinkV2.Models.ComplaintEventHistory", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.Complaint", "Complaint")
                        .WithMany("EventHistory")
                        .HasForeignKey("ComplaintId");

                    b.HasOne("NestLinkV2.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId");
                });

            modelBuilder.Entity("NestLinkV2.Models.Job", b =>
                {
                    b.HasOne("NestLinkV2.Models.Assignment", "Assignment")
                        .WithMany("Jobs")
                        .HasForeignKey("AssignmentId");

                    b.HasOne("NestLinkV2.Models.JobStatus", "JobStatus")
                        .WithMany()
                        .HasForeignKey("JobStatusId");

                    b.HasOne("NestLinkV2.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("NestLinkV2.Models.JobEventHistory", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId");

                    b.HasOne("NestLinkV2.Models.Job", "Job")
                        .WithMany("EventHistory")
                        .HasForeignKey("JobId");
                });

            modelBuilder.Entity("NestLinkV2.Models.Quote", b =>
                {
                    b.HasOne("NestLinkV2.Models.Assignment", "Assignment")
                        .WithMany("Quotes")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("NestLinkV2.Models.QuoteEventHistory", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId");

                    b.HasOne("NestLinkV2.Models.Quote", "Quote")
                        .WithMany("EventHistory")
                        .HasForeignKey("QuoteId");
                });

            modelBuilder.Entity("NestLinkV2.Models.Site", b =>
                {
                    b.HasOne("NestLinkV2.Models.Client", "Client")
                        .WithMany("Sites")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("NestLinkV2.Models.Technician", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.Job", "CurrentJob")
                        .WithMany()
                        .HasForeignKey("CurrentJobId");
                });

            modelBuilder.Entity("NestLinkV2.Models.WorkItem", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.Assignment", "Assignment")
                        .WithMany("WorkItems")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("NestLinkV2.Models.WorkItemEventHistory", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("AspNetUserId");

                    b.HasOne("NestLinkV2.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId");

                    b.HasOne("NestLinkV2.Models.WorkItem", "WorkItem")
                        .WithMany("EventHistory")
                        .HasForeignKey("WorkItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
