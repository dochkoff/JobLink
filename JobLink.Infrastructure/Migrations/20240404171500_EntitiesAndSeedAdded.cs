using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Migrations
{
    public partial class EntitiesAndSeedAdded : Migration
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
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Company identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Company Identifier")
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
                name: "ApplicantJob",
                columns: table => new
                {
                    ApplicantsId = table.Column<int>(type: "int", nullable: false),
                    JobsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantJob", x => new { x.ApplicantsId, x.JobsId });
                    table.ForeignKey(
                        name: "FK_ApplicantJob_Applicants_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantJob_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Application Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    { "7172d444-1cbb-4856-a0ec-6f47634693bc", 0, "020325be-305a-496a-ad53-faf66903300c", "sirmarecruit@sirma.com", false, false, null, "sirmarecruit@sirma.com", "sirmarecruit@sirma.com", "AQAAAAEAACcQAAAAENEwxazEaM1PalbCsdbkxev7IJPNfR2JaymarRhnfQEEPd23w/Vvqdj2OR3AOXEDuQ==", null, false, "f050978d-d0f8-44c8-86bd-5d86c923b893", false, "sirmarecruit@sirma.com" },
                    { "c7d63805-14d1-4a61-bbe8-a8abd51b5c32", 0, "51a2b9b9-7d25-49af-85b3-00c465d850d9", "guest@gmail.com", false, false, null, "guest@gmail.com", "guest@gmail.com", "AQAAAAEAACcQAAAAEMEfSPbpEvBDfk6cpUR3Uj4KUAirwQoMmf8PySMfAOZRH//uspRw+auyY6HOVssGLg==", null, false, "a8f75631-c3c3-46ba-a541-8635e66d0deb", false, "guest@gmail.com" },
                    { "d52c4853-4d5e-4be0-9639-80e17a54b87f", 0, "e3df3f02-d7e3-4dc2-aef5-8cab8fc5ddab", "needajob@abv.bg", false, false, null, "needajob@abv.bg", "needajob@abv.bg", "AQAAAAEAACcQAAAAEFfXu32lZScTJq6OT0K13PAf4/wfyDYHq4XJzfEApDygres9mwQjjycpM+EbyZs0Dw==", null, false, "021e01bf-a386-4671-b468-e515fa68b8f8", false, "needajob@abv.bg" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "LogoUrl", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { 1, "Sofia, Bulgaria", "https://3e-news.net/web/files/articles/37670/main_image/thumb_850x480_sirma-group-logo.jpg", "Sirma Solutions", "+359 2 976 8310", "https://sirma.com" },
                    { 2, "Boston, MA", "https://fontmeme.com/images/DraftKings-logo-font.png", "DraftKings", "+16175551212", "https://draftkings.com" }
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
                values: new object[] { 1, "Pavel Dochkov", "+359887654321", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "d52c4853-4d5e-4be0-9639-80e17a54b87f" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyId", "PhoneNumber", "UserId" },
                values: new object[] { 1, 1, "+359880000000", "7172d444-1cbb-4856-a0ec-6f47634693bc" });

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
                columns: new[] { "Id", "ApplicantId", "JobId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicantId", "JobId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantJob_JobsId",
                table: "ApplicantJob",
                column: "JobsId");

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
                name: "ApplicantJob");

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
                keyValue: "c7d63805-14d1-4a61-bbe8-a8abd51b5c32");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d52c4853-4d5e-4be0-9639-80e17a54b87f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7172d444-1cbb-4856-a0ec-6f47634693bc");
        }
    }
}
