using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a29be2b4-54dc-4094-8444-5042e306b878", 0, "2339494f-580e-49d0-a36a-4f7b174f5e33", "needajob@abv.bg", false, false, null, "needajob@abv.bg", "needajob@abv.bg", "AQAAAAEAACcQAAAAEOg/xkPAMFw+wp1hcyM5REW1Q+nzMIU6FuTztGGTAD0QYI82CwmxbgQm8e7ovUoBlQ==", null, false, "b3714e22-dda9-49a5-9ce2-f52a7754f40b", false, "needajob@abv.bg" },
                    { "c8743cab-0971-463a-874d-1c3f6f09a4f1", 0, "a33cd711-2003-4572-b47f-7ff32fd42eca", "guest@gmail.com", false, false, null, "guest@gmail.com", "guest@gmail.com", "AQAAAAEAACcQAAAAEMTrW7ABdlG+KRGeQwa9CZ8a1udGqbmlMOJfmo2XaD1LLaSZudJyQ4BGW0XhycKx/A==", null, false, "79e5e4a4-bc1b-4f21-8f2b-cfd7666b2c81", false, "guest@gmail.com" },
                    { "d019707c-65d5-4593-8b4c-0ba24f6a5892", 0, "f1263a74-5563-4a45-8733-b062652b20ae", "sirmarecruit@sirma.com", false, false, null, "sirmarecruit@sirma.com", "sirmarecruit@sirma.com", "AQAAAAEAACcQAAAAEH/c0mMAH9Yt9BBPlPEIu/t5a7TCk2bd5truGJ66ius4cuhoHHGxxJg6k3JUIc0cRA==", null, false, "f9df0007-a781-41f5-9169-f9cdcbf7ecfe", false, "sirmarecruit@sirma.com" }
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
                values: new object[] { 1, "Pavel Dochkov", "+359887654321", "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing", "a29be2b4-54dc-4094-8444-5042e306b878" });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "Address", "CompanyName", "LogoUrl", "PhoneNumber", "UserId", "Website" },
                values: new object[] { 1, "Sofia, Bulgaria", "Sirma Solutions", "https://github.com/dochkoff/JobLink/blob/8aba84771b5cfe445466c3f415c45dc612a359d7/JobLink/wwwroot/images/SirmaGroupLogo.jpg", "+359880000000", "d019707c-65d5-4593-8b4c-0ba24f6a5892", "https://sirma.com" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8743cab-0971-463a-874d-1c3f6f09a4f1");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Applicants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a29be2b4-54dc-4094-8444-5042e306b878");

            migrationBuilder.DeleteData(
                table: "Employers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d019707c-65d5-4593-8b4c-0ba24f6a5892");
        }
    }
}
