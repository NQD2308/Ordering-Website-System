using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86f9a1a8-7fa0-408b-9483-a6324598413d", "785d878f-6dbf-4734-9332-3757f35169d2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5c7f0dc3-47b1-464e-9042-0fa3d1e0db98", 0, "HCM", "39b45dae-41cd-4ab2-b205-ecde83861b00", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEOsS37MDyTYqZnr+OgnLRN9oMYbrdv5tAxRzmpfibkaerALVKji8GCGbojlImUX4TA==", "1234567890", false, "54e9646d-ece4-468a-a7e1-70617bfb6574", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "86f9a1a8-7fa0-408b-9483-a6324598413d", "5c7f0dc3-47b1-464e-9042-0fa3d1e0db98" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "86f9a1a8-7fa0-408b-9483-a6324598413d", "5c7f0dc3-47b1-464e-9042-0fa3d1e0db98" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86f9a1a8-7fa0-408b-9483-a6324598413d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5c7f0dc3-47b1-464e-9042-0fa3d1e0db98");
        }
    }
}
