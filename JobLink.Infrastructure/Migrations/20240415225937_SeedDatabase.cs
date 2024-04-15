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
                values: new object[] { "b0b26784-bf8a-46f8-9fde-bb3d674c62b2", "7f61e4c6-a329-4379-ad10-fa955925439c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "30121835-deea-4cfe-818b-f44588310481", 0, "32fa4eb4-eaf7-42cb-a4ae-15ddc4c9345d", "AccountHolder", "needajob@abv.bg", false, "Pavel", "Dochkov", false, null, "NEEDAJOB@ABV.BG", "NEEDAJOB@ABV.BG", "AQAAAAEAACcQAAAAEPGR2cN3DjtxsQttKAzSMAjv1CiTtj2WYLWEhJ0pPftLgHKYv3JbmYa/UgRavpQhEg==", null, false, "8be99598-71a0-4953-aae4-9bcf4f178ba1", false, "needajob@abv.bg" },
                    { "7a29bd89-4ed7-417c-9663-cc461dfd03e3", 0, "92240f1b-252f-4b01-ba84-2c5783e4be91", "AccountHolder", "sirmarecruit@sirma.com", false, "Stamo", "Blagodarya", false, null, "SIRMARECRUIT@SIRMA.COM", "SIRMARECRUIT@SIRMA.COM", "AQAAAAEAACcQAAAAEACVGtUz3GwAOEjNuOg42h5qGeELRYsnRGarTOUpDXIGDiYs3YkRgUB89i9Vo7N4ZA==", null, false, "9b389320-a87d-4a44-8415-7743cd8aad38", false, "sirmarecruit@sirma.com" },
                    { "8f06c7b6-1e5a-4fa8-885a-dbdd2a40c076", 0, "3d3e86ec-fd6a-4c4b-a7e2-7bc105742a6e", "AccountHolder", "newuser@gmail.com", false, "Stefan", "Gorchev", false, null, "NEWUSER@GMAIL.COM", "NEWUSER@GMAIL.COM", "AQAAAAEAACcQAAAAEKfl7e1TcpOtGYMNuMHpusK3KpseM8PEwATUNApJl0VxM2x/R//Zbwc2aBqvzNFJTA==", null, false, "0193d357-35a4-450c-bba8-cba1235c0899", false, "newuser@gmail.com" },
                    { "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4", 0, "d9ba91a1-f200-4164-82cb-68234d0c364b", "AccountHolder", "admin@joblink.com", false, "Strict", "Admin", false, null, "ADMIN@JOBLINK.COM", "ADMIN@JOBLINK.COM", "AQAAAAEAACcQAAAAEGaXWLiK4zYC1QDRTa/YhlSAyhiLIqsRhhC6LS/ntIWkEccw0K5bGYGN/WSBvErMFQ==", null, false, "e835d3e8-c63b-455b-919c-2e83d7b12491", false, "admin@joblink.com" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "IsApproved", "LogoUrl", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { new Guid("5c33a6ba-3946-4157-8da6-d3a273d5775b"), "Sofia, Bulgaria", true, "/images/logos/logo-sirma.jpg", "Sirma Solutions", "+359 2 976 8310", "https://sirma.com" },
                    { new Guid("f14429b6-2fd4-4bd1-8fbf-d7c55264252d"), "Boston, MA", true, "/images/logos/logo-draftkings.png", "DraftKings", "+16175551212", "https://draftkings.com" }
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
                values: new object[] { 1, "+359886509188", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "30121835-deea-4cfe-818b-f44588310481" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "user:fullname", "Strict Admin", "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4" },
                    { 2, "user:fullname", "Stamo Blagodarya", "7a29bd89-4ed7-417c-9663-cc461dfd03e3" },
                    { 3, "user:fullname", "Pavel Dochkov", "30121835-deea-4cfe-818b-f44588310481" },
                    { 4, "user:fullname", "Stefan Gorchev", "8f06c7b6-1e5a-4fa8-885a-dbdd2a40c076" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b0b26784-bf8a-46f8-9fde-bb3d674c62b2", "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyId", "PhoneNumber", "UserId" },
                values: new object[] { 1, new Guid("5c33a6ba-3946-4157-8da6-d3a273d5775b"), "+359880000000", "7a29bd89-4ed7-417c-9663-cc461dfd03e3" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 1, 1, "Develop software with ASP.NET Core with C#", 1, "Remote", 5000m, "Software Developer" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 2, 2, "Sell our software platform internationaly", 1, "Sofia, Bulgaria", 3000m, "Sales Representative" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "Description", "EmployerId", "Location", "Salary", "Title" },
                values: new object[] { 3, 3, "Do accounting for Sirma Solutions staff", 1, "Kazanlak, Bulgaria", 2500m, "Accountant" });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 1, 1, new DateTime(2024, 4, 16, 1, 59, 36, 968, DateTimeKind.Local).AddTicks(7196), 1 });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 2, 1, new DateTime(2024, 4, 16, 1, 59, 36, 968, DateTimeKind.Local).AddTicks(7261), 2 });

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
                keyValues: new object[] { "b0b26784-bf8a-46f8-9fde-bb3d674c62b2", "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0b26784-bf8a-46f8-9fde-bb3d674c62b2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f06c7b6-1e5a-4fa8-885a-dbdd2a40c076");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a83adf52-1b7f-42f7-bf93-a2e4c1f0a7b4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30121835-deea-4cfe-818b-f44588310481");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a29bd89-4ed7-417c-9663-cc461dfd03e3");

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
