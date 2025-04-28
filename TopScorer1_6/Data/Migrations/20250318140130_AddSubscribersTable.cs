using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopScorer1_6.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscribersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caa18519-8322-4fef-a2e8-8616bf0599f3", "AQAAAAIAAYagAAAAEFl9OrofKXmK4OhNIuPeFAEF2Pn4zM1rILPght1wmwVC9SN54Snkxsoc7YOP7CxZLg==", "68816e9e-8149-45e4-96dc-6178357396b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4368a985-51d6-406d-ab47-f33d677bc82c", "AQAAAAIAAYagAAAAEHPz1i1cgY7Tepk0M7ev8YNl9Xihxz/YfH8WLq+L9Vw9F2yro3uNmNDp9ovltenQ/A==", "c2cd1d12-ccb7-4d14-93ce-99a17946e26c" });
        }
    }
}
