﻿// <auto-generated />
using System;
using JobLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobLink.Infrastructure.Migrations
{
    [DbContext(typeof(JobLinkDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Applicant Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Agent's phone");

                    b.Property<string>("ResumeUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Applicant's resume");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Applicants");

                    b.HasComment("Applicant for a job");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PhoneNumber = "+359886509188",
                            ResumeUrl = "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing",
                            UserId = "30121835-deea-4cfe-818b-f44588310481"
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Application Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int")
                        .HasComment("Applicant Identifier");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2")
                        .HasComment("Application Date and Time");

                    b.Property<int>("JobId")
                        .HasColumnType("int")
                        .HasComment("Job Identifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("JobId");

                    b.ToTable("Applications");

                    b.HasComment("Application for a job");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicantId = 1,
                            DateAndTime = new DateTime(2024, 4, 16, 1, 59, 36, 968, DateTimeKind.Local).AddTicks(7196),
                            JobId = 1
                        },
                        new
                        {
                            Id = 2,
                            ApplicantId = 1,
                            DateAndTime = new DateTime(2024, 4, 16, 1, 59, 36, 968, DateTimeKind.Local).AddTicks(7261),
                            JobId = 2
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Company identifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Company address");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit")
                        .HasComment("Is company active or not");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Company's logo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Company name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Company's phone number");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Company's website");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Companies");

                    b.HasComment("Company");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5c33a6ba-3946-4157-8da6-d3a273d5775b"),
                            Address = "Sofia, Bulgaria",
                            IsApproved = true,
                            LogoUrl = "/images/logos/logo-sirma.jpg",
                            Name = "Sirma Solutions",
                            PhoneNumber = "+359 2 976 8310",
                            Website = "https://sirma.com"
                        },
                        new
                        {
                            Id = new Guid("f14429b6-2fd4-4bd1-8fbf-d7c55264252d"),
                            Address = "Boston, MA",
                            IsApproved = true,
                            LogoUrl = "/images/logos/logo-draftkings.png",
                            Name = "DraftKings",
                            PhoneNumber = "+16175551212",
                            Website = "https://draftkings.com"
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Employer identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Company Identifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Employer's phone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Employers");

                    b.HasComment("Jobs Employer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = new Guid("5c33a6ba-3946-4157-8da6-d3a273d5775b"),
                            PhoneNumber = "+359880000000",
                            UserId = "7a29bd89-4ed7-417c-9663-cc461dfd03e3"
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Job identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Category identifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Job description");

                    b.Property<int>("EmployerId")
                        .HasColumnType("int")
                        .HasComment("Employer identifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Job location");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Monthly salary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Job title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EmployerId");

                    b.ToTable("Jobs");

                    b.HasComment("A job posting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Develop software with ASP.NET Core with C#",
                            EmployerId = 1,
                            Location = "Remote",
                            Salary = 5000m,
                            Title = "Software Developer"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Sell our software platform internationaly",
                            EmployerId = 1,
                            Location = "Sofia, Bulgaria",
                            Salary = 3000m,
                            Title = "Sales Representative"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Do accounting for Sirma Solutions staff",
                            EmployerId = 1,
                            Location = "Kazanlak, Bulgaria",
                            Salary = 2500m,
                            Title = "Accountant"
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.JobCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Category Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Category name");

                    b.HasKey("Id");

                    b.ToTable("JobCategories");

                    b.HasComment("Job category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Software Development"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sales"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Finance"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "b0b26784-bf8a-46f8-9fde-bb3d674c62b2",
                            ConcurrencyStamp = "7f61e4c6-a329-4379-ad10-fa955925439c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
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

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "user:fullname",
                            ClaimValue = "Strict Admin",
                            UserId = "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "user:fullname",
                            ClaimValue = "Stamo Blagodarya",
                            UserId = "7a29bd89-4ed7-417c-9663-cc461dfd03e3"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "user:fullname",
                            ClaimValue = "Pavel Dochkov",
                            UserId = "30121835-deea-4cfe-818b-f44588310481"
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "user:fullname",
                            ClaimValue = "Stefan Gorchev",
                            UserId = "8f06c7b6-1e5a-4fa8-885a-dbdd2a40c076"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4",
                            RoleId = "b0b26784-bf8a-46f8-9fde-bb3d674c62b2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.AccountHolder", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasDiscriminator().HasValue("AccountHolder");

                    b.HasData(
                        new
                        {
                            Id = "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d9ba91a1-f200-4164-82cb-68234d0c364b",
                            Email = "admin@joblink.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@JOBLINK.COM",
                            NormalizedUserName = "ADMIN@JOBLINK.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGaXWLiK4zYC1QDRTa/YhlSAyhiLIqsRhhC6LS/ntIWkEccw0K5bGYGN/WSBvErMFQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e835d3e8-c63b-455b-919c-2e83d7b12491",
                            TwoFactorEnabled = false,
                            UserName = "admin@joblink.com",
                            FirstName = "Strict",
                            LastName = "Admin"
                        },
                        new
                        {
                            Id = "7a29bd89-4ed7-417c-9663-cc461dfd03e3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "92240f1b-252f-4b01-ba84-2c5783e4be91",
                            Email = "sirmarecruit@sirma.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "SIRMARECRUIT@SIRMA.COM",
                            NormalizedUserName = "SIRMARECRUIT@SIRMA.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEACVGtUz3GwAOEjNuOg42h5qGeELRYsnRGarTOUpDXIGDiYs3YkRgUB89i9Vo7N4ZA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9b389320-a87d-4a44-8415-7743cd8aad38",
                            TwoFactorEnabled = false,
                            UserName = "sirmarecruit@sirma.com",
                            FirstName = "Stamo",
                            LastName = "Blagodarya"
                        },
                        new
                        {
                            Id = "30121835-deea-4cfe-818b-f44588310481",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "32fa4eb4-eaf7-42cb-a4ae-15ddc4c9345d",
                            Email = "needajob@abv.bg",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "NEEDAJOB@ABV.BG",
                            NormalizedUserName = "NEEDAJOB@ABV.BG",
                            PasswordHash = "AQAAAAEAACcQAAAAEPGR2cN3DjtxsQttKAzSMAjv1CiTtj2WYLWEhJ0pPftLgHKYv3JbmYa/UgRavpQhEg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8be99598-71a0-4953-aae4-9bcf4f178ba1",
                            TwoFactorEnabled = false,
                            UserName = "needajob@abv.bg",
                            FirstName = "Pavel",
                            LastName = "Dochkov"
                        },
                        new
                        {
                            Id = "8f06c7b6-1e5a-4fa8-885a-dbdd2a40c076",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3d3e86ec-fd6a-4c4b-a7e2-7bc105742a6e",
                            Email = "newuser@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "NEWUSER@GMAIL.COM",
                            NormalizedUserName = "NEWUSER@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKfl7e1TcpOtGYMNuMHpusK3KpseM8PEwATUNApJl0VxM2x/R//Zbwc2aBqvzNFJTA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0193d357-35a4-450c-bba8-cba1235c0899",
                            TwoFactorEnabled = false,
                            UserName = "newuser@gmail.com",
                            FirstName = "Stefan",
                            LastName = "Gorchev"
                        });
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Applicant", b =>
                {
                    b.HasOne("JobLink.Infrastructure.Data.Models.AccountHolder", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Application", b =>
                {
                    b.HasOne("JobLink.Infrastructure.Data.Models.Applicant", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobLink.Infrastructure.Data.Models.Job", "Job")
                        .WithMany("Applications")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Employer", b =>
                {
                    b.HasOne("JobLink.Infrastructure.Data.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobLink.Infrastructure.Data.Models.AccountHolder", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Job", b =>
                {
                    b.HasOne("JobLink.Infrastructure.Data.Models.JobCategory", "JobCategory")
                        .WithMany("Jobs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobLink.Infrastructure.Data.Models.Employer", "Employer")
                        .WithMany("Jobs")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("JobCategory");
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

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Applicant", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Employer", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.Job", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("JobLink.Infrastructure.Data.Models.JobCategory", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
