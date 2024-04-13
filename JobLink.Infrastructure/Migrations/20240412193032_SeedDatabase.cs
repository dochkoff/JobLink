using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Applicant Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Applicant name"),
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
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Company's logo")
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category name")
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "52ffe97b-c0d2-4747-9758-c42f45b819f3", 0, "6ccc27c3-94ca-4688-a2c8-1f73a05092bd", "guest@gmail.com", false, false, null, "guest@gmail.com", "guest@gmail.com", "AQAAAAEAACcQAAAAEGm4ZgIyiHYRUV04qkUbXrsVlRgFvh53iUzafQiPPKHiw83nbnSL/jvECqOkKrmR1Q==", null, false, "846deae3-de10-4cbd-9ffd-a359ed318211", false, "guest@gmail.com" },
                    { "76b3f545-e38c-4ee9-b7ee-1641c81ef9ad", 0, "e4b6a499-f936-4361-b379-45bc366467a2", "needajob@abv.bg", false, false, null, "needajob@abv.bg", "needajob@abv.bg", "AQAAAAEAACcQAAAAEDsOTlpj6bGyLwRtEfN9GZXMidQEkH5etGjRX7zBH3C+OzDP1jHD6a4UcHkFm0H+SA==", null, false, "86d208ae-c179-4e4a-a30c-5620fd150af9", false, "needajob@abv.bg" },
                    { "8c938716-90c9-4e2d-9c1e-eed5c13e6566", 0, "e19b64b3-177e-44d7-a3cf-e9519a119ccd", "sirmarecruit@sirma.com", false, false, null, "sirmarecruit@sirma.com", "sirmarecruit@sirma.com", "AQAAAAEAACcQAAAAECv9O+DRe/M4PBWrp2P7BSYxiCuQq5+KY/tjoIR/Xi8yMTOqnoERR2iyA0Wd1O0tBA==", null, false, "95fafc5d-ab79-44c8-b693-8b961cd5df7e", false, "sirmarecruit@sirma.com" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "LogoUrl", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { new Guid("145039e7-c7be-4d86-b7d1-cad0bcd791fe"), "Sofia, Bulgaria", "logo-sirma.jpg", "Sirma Solutions", "+359 2 976 8310", "https://sirma.com" },
                    { new Guid("fd097e8e-5034-4394-9673-3492c231e664"), "Boston, MA", "logo-draftkings.png", "DraftKings", "+16175551212", "https://draftkings.com" }
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
                columns: new[] { "Id", "Name", "PhoneNumber", "ResumeUrl", "UserId" },
                values: new object[] { 1, "Pavel Dochkov", "+359887654321", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "76b3f545-e38c-4ee9-b7ee-1641c81ef9ad" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyId", "PhoneNumber", "UserId" },
                values: new object[] { 1, new Guid("145039e7-c7be-4d86-b7d1-cad0bcd791fe"), "+359880000000", "8c938716-90c9-4e2d-9c1e-eed5c13e6566" });

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
                values: new object[] { 1, 1, new DateTime(2024, 4, 12, 22, 30, 32, 498, DateTimeKind.Local).AddTicks(6626), 1 });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 2, 1, new DateTime(2024, 4, 12, 22, 30, 32, 498, DateTimeKind.Local).AddTicks(6707), 2 });

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
                name: "IX_Companies_PhoneNumber",
                table: "Companies",
                column: "PhoneNumber",
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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ffe97b-c0d2-4747-9758-c42f45b819f3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76b3f545-e38c-4ee9-b7ee-1641c81ef9ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c938716-90c9-4e2d-9c1e-eed5c13e6566");
        }
    }
}
