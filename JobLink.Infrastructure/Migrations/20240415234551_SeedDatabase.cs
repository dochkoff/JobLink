using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Applicant Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Agent's phone"),
                    ResumeUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Applicant's resume"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Applicant for a job");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Company identifier"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Company name"),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Company address"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Company's phone number"),
                    Website = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Company's website"),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Company's logo"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, comment: "Is company active or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                },
                comment: "Company");

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Category Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                },
                comment: "Job category");

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Employer identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Employer's phone"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Identifier"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Company Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Jobs Employer");

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Job identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Job title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Job description"),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Job location"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Monthly salary"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Category identifier"),
                    EmployerId = table.Column<int>(type: "int", nullable: false, comment: "Employer identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_JobCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "A job posting");

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Application Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Application Date and Time"),
                    JobId = table.Column<int>(type: "int", nullable: false, comment: "Job Identifier"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false, comment: "Applicant Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Application for a job");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc3a5443-aea5-441f-935f-92311a9f6bf7", "02ae0562-aea2-45eb-8f55-52b8e394b4b5", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2b487f26-1743-412f-9345-6e5cd5e71825", 0, "ec96c803-e012-4dc4-8f3d-86c6445baf22", "AccountHolder", "admin@joblink.com", false, "Strict", "Admin", false, null, "ADMIN@JOBLINK.COM", "ADMIN@JOBLINK.COM", "AQAAAAEAACcQAAAAEM4X2wzC/zH1rhfUfwaWlavxEcJ/h/EPu8hCKtl7DwWhsFQmJXLRSVm/RBhi2iFvkg==", null, false, "f77d3415-8f12-435c-86a3-6da479a626b1", false, "admin@joblink.com" },
                    { "59041825-f863-4f8e-a794-665ef803a853", 0, "ab476698-2450-4207-a3c0-e7f0d76ca38d", "AccountHolder", "needajob@abv.bg", false, "Pavel", "Dochkov", false, null, "NEEDAJOB@ABV.BG", "NEEDAJOB@ABV.BG", "AQAAAAEAACcQAAAAEKiVFyPUCnTaOkM5aUxk568s7b4XJPQZySftBSuL5zL/Jej8ByrMjcom4x+jam6erQ==", null, false, "c9bc13b8-0e75-4753-8fad-437b9f4615ff", false, "needajob@abv.bg" },
                    { "957c5c95-e722-426a-9d9b-8a34776e182a", 0, "be40291b-ae73-4e71-9502-52e6bba4d0b5", "AccountHolder", "sirmarecruit@sirma.com", false, "Stamo", "Blagodarya", false, null, "SIRMARECRUIT@SIRMA.COM", "SIRMARECRUIT@SIRMA.COM", "AQAAAAEAACcQAAAAECbPkc6S515cgdYhBiDtgaznY7bOJ1V6F70jcpAqfjmuXryXJyKbTn9MIhWbqMPceg==", null, false, "bbf9ea05-3d60-4be7-bd63-a5a688c5f34e", false, "sirmarecruit@sirma.com" },
                    { "c1f3c318-dc77-442c-b3a9-f1dcea91f261", 0, "469a1e3b-1ffc-4bfd-b1b0-053842847e53", "AccountHolder", "newuser@gmail.com", false, "Stefan", "Gorchev", false, null, "NEWUSER@GMAIL.COM", "NEWUSER@GMAIL.COM", "AQAAAAEAACcQAAAAEJXM5U93Vo1M4PckkKubX/VbDVMhrskWfaHi0upPHpmkLcFA+Lv+dSC/S1eiFHJbtA==", null, false, "f6bb8451-7e62-4305-82eb-5d868c0c61ee", false, "newuser@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "IsApproved", "LogoUrl", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { new Guid("13c1a7c4-88b4-48d4-a2f4-221c813f8236"), "Stara Zagora, Bulgaria", true, "/images/logos/logo-joblink.jpg", "JobLink", "+359 42 333 999", "https://joblink.com" },
                    { new Guid("42b4f270-7dbe-4097-a066-b262841ff83c"), "Boston, MA", true, "/images/logos/logo-draftkings.png", "DraftKings", "+16175551212", "https://draftkings.com" },
                    { new Guid("c90e05de-856a-4ace-ad03-b437e333ce4f"), "Sofia, Bulgaria", true, "/images/logos/logo-sirma.jpg", "Sirma Solutions", "+359 2 976 8310", "https://sirma.com" }
                });

            migrationBuilder.InsertData(
                table: "JobCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Software Development" },
                    { 2, "Sales" },
                    { 3, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Applicants",
                columns: new[] { "Id", "PhoneNumber", "ResumeUrl", "UserId" },
                values: new object[] { 1, "+359886509188", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "59041825-f863-4f8e-a794-665ef803a853" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "user:fullname", "Strict Admin", "2b487f26-1743-412f-9345-6e5cd5e71825" },
                    { 2, "user:fullname", "Stamo Blagodarya", "957c5c95-e722-426a-9d9b-8a34776e182a" },
                    { 3, "user:fullname", "Pavel Dochkov", "59041825-f863-4f8e-a794-665ef803a853" },
                    { 4, "user:fullname", "Stefan Gorchev", "c1f3c318-dc77-442c-b3a9-f1dcea91f261" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dc3a5443-aea5-441f-935f-92311a9f6bf7", "2b487f26-1743-412f-9345-6e5cd5e71825" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyId", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("13c1a7c4-88b4-48d4-a2f4-221c813f8236"), "+359880000000", "2b487f26-1743-412f-9345-6e5cd5e71825" },
                    { 2, new Guid("c90e05de-856a-4ace-ad03-b437e333ce4f"), "+359887654321", "957c5c95-e722-426a-9d9b-8a34776e182a" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 1, 1, "Develop software with ASP.NET Core with C#", 2, "Remote", 5000m, "Software Developer" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 2, 2, "Sell our software platform internationaly", 2, "Sofia, Bulgaria", 3000m, "Sales Representative" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 3, 3, "Do accounting for Sirma Solutions staff", 2, "Kazanlak, Bulgaria", 2500m, "Accountant" });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 1, 1, new DateTime(2024, 4, 16, 2, 45, 50, 633, DateTimeKind.Local).AddTicks(5551), 1 });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 2, 1, new DateTime(2024, 4, 13, 2, 45, 50, 633, DateTimeKind.Local).AddTicks(5611), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_PhoneNumber",
                table: "Applicants",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_UserId",
                table: "Applicants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_CompanyId",
                table: "Employers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_PhoneNumber",
                table: "Employers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployerId",
                table: "Jobs",
                column: "EmployerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc3a5443-aea5-441f-935f-92311a9f6bf7", "2b487f26-1743-412f-9345-6e5cd5e71825" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3a5443-aea5-441f-935f-92311a9f6bf7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b487f26-1743-412f-9345-6e5cd5e71825");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c1f3c318-dc77-442c-b3a9-f1dcea91f261");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59041825-f863-4f8e-a794-665ef803a853");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "957c5c95-e722-426a-9d9b-8a34776e182a");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
