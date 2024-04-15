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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00373d8d-f565-4bd6-b0f3-f62fa8720eeb", 0, "0582a2c2-4702-4f14-9846-16f423068a3d", "AccountHolder", "needajob@abv.bg", false, "Pavel", "Dochkov", false, null, "NEEDAJOB@ABV.BG", "NEEDAJOB@ABV.BG", "AQAAAAEAACcQAAAAEH/ehvS0GVo4C4nk1FXDc33FYh3BTvFkFgE1PudBKE3PeXR6CcisLFbQLibDVIDErA==", null, false, "0c9d6e96-cbe4-4d7d-ada1-9d0075acf3f2", false, "needajob@abv.bg" },
                    { "2001951d-763b-48aa-af33-e6e65c970b9e", 0, "281f1861-6722-462a-a26b-7ad1c210ffc2", "AccountHolder", "sirmarecruit@sirma.com", false, "Stamo", "Blagodarya", false, null, "SIRMARECRUIT@SIRMA.COM", "SIRMARECRUIT@SIRMA.COM", "AQAAAAEAACcQAAAAEFbWSDnJVvGEC+yZSLlwa5LJH7zX9mmXpLlxTJ+AxyhgVNmBGLc8uq7juiHvsm9JgQ==", null, false, "4d51069c-0ef2-4877-9b7c-a0750a96f36c", false, "sirmarecruit@sirma.com" },
                    { "e0600ce0-2c0a-47d7-91db-cf058cfe5d08", 0, "df309bec-e817-40a7-a3ca-86e52769462a", "AccountHolder", "guest@gmail.com", false, "Stefan", "Gorchev", false, null, "guest@gmail.com", "guest@gmail.com", "AQAAAAEAACcQAAAAENi/19rzAeDdFmm+SuFE8QVgrhkH4QF/bYsnACcqU+HlnWfhYKBlelwGb0ZIMbO52A==", null, false, "8b855fad-3993-4a96-8bdd-8d96bb30b8b2", false, "guest@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "IsApproved", "LogoUrl", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { new Guid("3ac3695f-9199-4a9e-8a20-41ba1f2f6d87"), "Boston, MA", true, "/images/logos/logo-draftkings.png", "DraftKings", "+16175551212", "https://draftkings.com" },
                    { new Guid("7ad0d540-9739-41e2-8556-ce46f32cf438"), "Sofia, Bulgaria", true, "/images/logos/logo-sirma.jpg", "Sirma Solutions", "+359 2 976 8310", "https://sirma.com" }
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
                values: new object[] { 1, "+359886509188", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "00373d8d-f565-4bd6-b0f3-f62fa8720eeb" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyId", "PhoneNumber", "UserId" },
                values: new object[] { 1, new Guid("7ad0d540-9739-41e2-8556-ce46f32cf438"), "+359880000000", "2001951d-763b-48aa-af33-e6e65c970b9e" });

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
                values: new object[] { 1, 1, new DateTime(2024, 4, 15, 22, 2, 17, 148, DateTimeKind.Local).AddTicks(9183), 1 });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "DateAndTime", "JobId" },
                values: new object[] { 2, 1, new DateTime(2024, 4, 15, 22, 2, 17, 148, DateTimeKind.Local).AddTicks(9245), 2 });

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0600ce0-2c0a-47d7-91db-cf058cfe5d08");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00373d8d-f565-4bd6-b0f3-f62fa8720eeb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2001951d-763b-48aa-af33-e6e65c970b9e");

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
