using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopScorer1_6.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "4368a985-51d6-406d-ab47-f33d677bc82c", "admin1@mail.com", true, false, null, "ADMIN1@MAIL.COM", "ADMIN1@MAIL.COM", "AQAAAAIAAYagAAAAEHPz1i1cgY7Tepk0M7ev8YNl9Xihxz/YfH8WLq+L9Vw9F2yro3uNmNDp9ovltenQ/A==", null, false, "c2cd1d12-ccb7-4d14-93ce-99a17946e26c", false, "Admin1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
